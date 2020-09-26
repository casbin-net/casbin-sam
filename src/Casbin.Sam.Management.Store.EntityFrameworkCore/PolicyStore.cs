using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using NetCasbin;
using NetCasbin.Model;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class PolicyStore : IPolicyStore<SamPolicy>
    {
        private readonly ICasbinSamModelCache<CasbinSamModel> _casbinModelCache;
        private readonly SamAdapterProvider _samAdapterProvider;
        private readonly IVersionTokenProvider<CasbinSamModel, string> _versionTokenProvider;
        private readonly Enforcer _enforcer = new Enforcer();

        public PolicyStore(ICasbinSamModelCache<CasbinSamModel> casbinModelCache,
            SamAdapterProvider samAdapterProvider,
            IVersionTokenProvider<CasbinSamModel, string> versionTokenProvider)
        {
            _casbinModelCache = casbinModelCache ?? throw new ArgumentNullException(nameof(casbinModelCache));
            _samAdapterProvider = samAdapterProvider ?? throw new ArgumentNullException(nameof(samAdapterProvider));
            _versionTokenProvider = versionTokenProvider ?? throw new ArgumentNullException(nameof(versionTokenProvider));
        }

        public async Task<SamPolicy> AddPolicyAsync(string scopeId, SamPolicy policy)
        {
            var samModel = await GetSamModelAsync(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.AddNamedPolicyAsync(policy.Type, policy.Rule.ToArray());
            await UpdateVersionTokenAsync(scopeId, samModel);
            return policy;
        }

        public async ValueTask<IEnumerable<SamPolicy>> GetFilteredPoliciesAsync(string scopeId, string policyType, FilterParameter parameter)
        {
            var samModel = await GetSamModelAsync(scopeId);
            var policies = samModel.Model.GetFilteredPolicy(PermConstants.Section.PolicySection,
                policyType, parameter.StartIndex, parameter.Values as string[] ?? parameter.Values.ToArray());
            return policies.Select(p => new SamPolicy(policyType, p));
        }

        public async ValueTask<IEnumerable<SamPolicy>> GetPoliciesAsync(string scopeId, string policyType)
        {
            var samModel = await GetSamModelAsync(scopeId);
            var policies = samModel.Model.GetPolicy(PermConstants.Section.PolicySection, policyType);
            return policies.Select(p => new SamPolicy(policyType, p));
        }

        public async Task RemovePolicyAsync(string scopeId, SamPolicy policy)
        {
            var samModel = await GetSamModelAsync(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.RemoveNamedPolicyAsync(policy.Type, policy.Rule as string[] ?? policy.Rule.ToArray());
        }

        public async Task RemoveFilteredPoliciesAsync(string scopeId, string policyType, FilterParameter parameter)
        {
            var samModel = await GetSamModelAsync(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.RemoveFilteredNamedPolicyAsync(policyType, parameter.StartIndex, parameter.Values as string[] ?? parameter.Values.ToArray());
        }

        private async ValueTask<CasbinSamModel> GetSamModelAsync(string scopeId)
        {
            if (_casbinModelCache.TryGetModel(scopeId, out var samModel))
            {
                return samModel;
            }

            samModel = new CasbinSamModel(scopeId, CoreEnforcer.NewModel(),
                await _versionTokenProvider.GenerateVersionTokenAsync());

            return _casbinModelCache.AddOrUpdateModel(scopeId, samModel);
        }

        private Enforcer GetEnforcer(string scopeId, CasbinSamModel samModel)
        {
            _enforcer.SetModel(samModel.Model);
            _enforcer.SetAdapter(_samAdapterProvider.GetAdapter(scopeId));
            return _enforcer;
        }

        private async Task UpdateVersionTokenAsync(string scopeId, CasbinSamModel samModel)
        {
            samModel = new CasbinSamModel(scopeId, samModel.Model,
                await _versionTokenProvider.GenerateVersionTokenAsync(samModel));
            _casbinModelCache.AddOrUpdateModel(scopeId, samModel);
        }
    }
}
