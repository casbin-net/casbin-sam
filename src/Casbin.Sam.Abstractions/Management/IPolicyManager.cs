using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IPolicyManager<T>
    {
        public ValueTask<IEnumerable<SamPolicy>> GetPolicies(string policyType);

        public ValueTask<IEnumerable<SamPolicy>> GetPolicies(string scopeId, string policyType);

        public Task<T> AddPolicyAsync(T policy);

        public Task<T> RemovePolicyAsync(T policy);

        public Task RemoveFilteredPolicyAsync(string policyType, FilterParameter parameter);

        public Task<T> AddPolicyAsync(string scopeId,T policy);

        public Task<T> RemovePolicyAsync(string scopeId,T policy);

        public Task RemoveFilteredPolicyAsync(string scopeId,string policyType, FilterParameter parameter);
    }
}
