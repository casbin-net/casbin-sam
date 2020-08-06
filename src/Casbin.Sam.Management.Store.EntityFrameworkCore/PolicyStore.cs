using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class PolicyStore : IPolicyStore<Policy>
    {
        public Task<Policy> AddPolicyAsync(string scopeId, Policy policy)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<IEnumerable<Policy>> GetFilteredPoliciesAsync(string scopeId, Policy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<IEnumerable<Policy>> GetPoliciesAsync(string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFilteredPoliciesAsync(string scopeId, Policy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePolicyAsync(string scopeId, Policy policy)
        {
            throw new System.NotImplementedException();
        }
    }
}
