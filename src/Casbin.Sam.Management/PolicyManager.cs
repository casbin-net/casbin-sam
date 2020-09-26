using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class PolicyManager : IPolicyManager<SamPolicy>
    {
        public Task<SamPolicy> AddPolicyAsync(SamPolicy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<SamPolicy> AddPolicyAsync(string scopeId, SamPolicy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<SamPolicy> DeleteFilteredPolicyAsync(SamPolicy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<SamPolicy> DeleteFilteredPolicyAsync(string scopeId, SamPolicy policy, FilterParameter parameter)
        {
            throw new System.NotImplementedException();
        }

        public Task<SamPolicy> DeletePolicyAsync(SamPolicy policy)
        {
            throw new System.NotImplementedException();
        }

        public Task<SamPolicy> DeletePolicyAsync(string scopeId, SamPolicy policy)
        {
            throw new System.NotImplementedException();
        }
    }
}
