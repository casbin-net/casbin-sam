using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management.Store
{
    public interface IPolicyStore<T>
    {
        public ValueTask<IEnumerable<T>> GetPoliciesAsync(string scopeId);

        public Task<T> AddPolicyAsync(string scopeId, T policy);

        public Task RemovePolicyAsync(string scopeId, T policy);

        public ValueTask<IEnumerable<T>> GetFilteredPoliciesAsync(string scopeId, T policy,
            FilterParameter parameter);

        public Task RemoveFilteredPoliciesAsync(string scopeId, T policy,
            FilterParameter parameter);
    }
}
