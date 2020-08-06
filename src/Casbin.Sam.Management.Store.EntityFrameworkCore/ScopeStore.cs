using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class ScopeStore : IScopeStore<AuthorizationScope>
    {
        public Task<AuthorizationScope> AddScopeAsync(AuthorizationScope scope)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> GetScopeAsync(string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveScopeAsync(string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> UpdateScopeAsync(string scopeId, AuthorizationScope scope)
        {
            throw new System.NotImplementedException();
        }
    }
}
