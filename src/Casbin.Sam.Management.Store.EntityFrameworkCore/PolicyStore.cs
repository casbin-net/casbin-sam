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
        private readonly ISamScopeModelCache<SamScopeModel> _scopeModelCache;
        private readonly SamAdapterProvider _samAdapterProvider;
        private readonly IVersionTokenProvider<SamScopeModel, string> _versionTokenProvider;
        private readonly Enforcer _enforcer = new Enforcer();

        public PolicyStore(ISamScopeModelCache<SamScopeModel> scopeModelCache,
            SamAdapterProvider samAdapterProvider,
            IVersionTokenProvider<SamScopeModel, string> versionTokenProvider)
        {
            _scopeModelCache = scopeModelCache ?? throw new ArgumentNullException(nameof(scopeModelCache));
            _samAdapterProvider = samAdapterProvider ?? throw new ArgumentNullException(nameof(samAdapterProvider));
            _versionTokenProvider = versionTokenProvider ?? throw new ArgumentNullException(nameof(versionTokenProvider));
        }

        public async Task<SamPolicy> AddPolicyAsync(string scopeId, SamPolicy policy)
        {
            var samModel = GetSamModel(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.AddNamedPolicyAsync(policy.Type, policy.Rule.ToArray());
            await UpdateVersionTokenAsync(scopeId, samModel);
            return policy;
        }

        public ValueTask<IEnumerable<SamPolicy>> GetFilteredPoliciesAsync(string scopeId, string policyType, FilterParameter parameter)
        {
            var samModel = GetSamModel(scopeId);
            var policies = samModel.Model.GetFilteredPolicy(PermConstants.Section.PolicySection,
                policyType, parameter.StartIndex, parameter.Values as string[] ?? parameter.Values.ToArray());
            return new ValueTask<IEnumerable<SamPolicy>>(policies.Select(p => new SamPolicy(policyType, p)));
        }

        public ValueTask<IEnumerable<SamPolicy>> GetPoliciesAsync(string scopeId, string policyType)
        {
            var samModel = GetSamModel(scopeId);
            var policies = samModel.Model.GetPolicy(PermConstants.Section.PolicySection, policyType);
            return new ValueTask<IEnumerable<SamPolicy>>(policies.Select(p => new SamPolicy(policyType, p)));
        }

        public async Task RemovePolicyAsync(string scopeId, SamPolicy policy)
        {
            var samModel = GetSamModel(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.RemoveNamedPolicyAsync(policy.Type, policy.Rule as string[] ?? policy.Rule.ToArray());
        }

        public async Task RemoveFilteredPoliciesAsync(string scopeId, string policyType, FilterParameter parameter)
        {
            var samModel = GetSamModel(scopeId);
            var enforcer = GetEnforcer(scopeId, samModel);
            await enforcer.RemoveFilteredNamedPolicyAsync(policyType, parameter.StartIndex, parameter.Values as string[] ?? parameter.Values.ToArray());
        }

        private SamScopeModel GetSamModel(string scopeId)
        {
            if (_scopeModelCache.TryGetModel(scopeId, out var samModel) is false)
            {
                throw new ApplicationException($"Can not find the special scope {scopeId}");
            }

            return samModel;
        }

        private Enforcer GetEnforcer(string scopeId, SamScopeModel samModel)
        {
            _enforcer.SetModel(samModel.Model);
            _enforcer.SetAdapter(_samAdapterProvider.GetAdapter(scopeId));
            return _enforcer;
        }

        private async Task UpdateVersionTokenAsync(string scopeId, SamScopeModel samModel)
        {
            samModel.VersionToken = await _versionTokenProvider.GenerateVersionTokenAsync(samModel);
            _scopeModelCache.AddOrUpdateModel(scopeId, samModel);
        }
    }
}
