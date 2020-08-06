using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class PolicyManager : IPolicyManager<Policy>
    {
        public Task<Policy> AddPolicyAsync(Policy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<Policy> AddPolicyAsync(string scopeId, Policy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<Policy> DeleteFilteredPolicyAsync(Policy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<Policy> DeleteFilteredPolicyAsync(string scopeId, Policy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<Policy> DeletePolicyAsync(Policy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<Policy> DeletePolicyAsync(string scopeId, Policy policy)
        {
            throw new System.NotImplementedException();
        }
    }
}
