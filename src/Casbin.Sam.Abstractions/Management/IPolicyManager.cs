using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IPolicyManager<T>
    {
        public Task<T> AddPolicyAsync(T policy);

        public Task<T> DeletePolicyAsync(T policy);

        public Task<T> DeleteFilteredPolicyAsync(T policy, FilterParameter parameter);

        public Task<T> AddPolicyAsync(string scopeId,T policy);

        public Task<T> DeletePolicyAsync(string scopeId,T policy);

        public Task<T> DeleteFilteredPolicyAsync(string scopeId,T policy, FilterParameter parameter);
    }
}
