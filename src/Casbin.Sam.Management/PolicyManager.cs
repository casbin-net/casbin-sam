using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store;

namespace Casbin.Sam.Management
{
    public class PolicyManager : IPolicyManager<SamPolicy>
    {
        private readonly IPolicyStore<SamPolicy> _policyStore;
        private readonly IScopeManager<AuthorizationScope> _scopeManager;

        public PolicyManager(IPolicyStore<SamPolicy> policyStore,
            IScopeManager<AuthorizationScope> scopeManager)
        {
            _policyStore = policyStore;
            _scopeManager = scopeManager;
        }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public ValueTask<IEnumerable<SamPolicy>> GetPolicies(string policyType)
        {
            return GetPolicies(SamConstants.DefaultAuthorizationScopeId, policyType);
        }

        public ValueTask<IEnumerable<SamPolicy>> GetPolicies(string scopeId, string policyType)
        {
            return _policyStore.GetPoliciesAsync(scopeId, policyType);
        }

        public Task<SamPolicy> AddPolicyAsync(SamPolicy policy)
        {
            return AddPolicyAsync(SamConstants.DefaultAuthorizationScopeId, policy);
        }

        public async Task<SamPolicy> AddPolicyAsync(string scopeId, SamPolicy policy)
        {
            await CheckScopeExistAsync(scopeId);
            return await _policyStore.AddPolicyAsync(scopeId, policy);
        }

        public Task<SamPolicy> RemovePolicyAsync(SamPolicy policy)
        {
            return RemovePolicyAsync(SamConstants.DefaultAuthorizationScopeId, policy);
        }

        public async Task<SamPolicy> RemovePolicyAsync(string scopeId, SamPolicy policy)
        {
            await CheckScopeExistAsync(scopeId);
            await _policyStore.RemovePolicyAsync(scopeId, policy);
            return policy;
        }

        public Task RemoveFilteredPolicyAsync(string policyType, FilterParameter parameter)
        {
            return RemoveFilteredPolicyAsync(SamConstants.DefaultAuthorizationScopeId, policyType, parameter);
        }

        public async Task RemoveFilteredPolicyAsync(string scopeId, string policyType, FilterParameter parameter)
        {
            await CheckScopeExistAsync(scopeId);
            await _policyStore.RemoveFilteredPoliciesAsync(scopeId, policyType, parameter);
        }

        protected virtual async Task CheckScopeExistAsync(string scopeId)
        {
            if (await _scopeManager.HasScopeAsync(scopeId))
            {
                return;
            }
            await _scopeManager.CreateScopeAsync(scopeId, scopeId);
        }
    }
}
