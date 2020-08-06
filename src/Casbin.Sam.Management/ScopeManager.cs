using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class ScopeManager : IScopeManager<AuthorizationScope>
    {
        public ValueTask<AuthorizationScope> GetScopeAsync(string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveScopeAsync(string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveScopeAsync(AuthorizationScope scope)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> AddClientAsync(AuthorizationScope scope, ProtectedClient client)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> AddClientsAsync(AuthorizationScope scope, IEnumerable<ProtectedClient> client)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> AddResourceAsync(AuthorizationScope scope, ProtectedResource resource)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> AddResourcesAsync(AuthorizationScope scope, IEnumerable<ProtectedResource> resources)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveClientAsync(AuthorizationScope scope, ProtectedClient client)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveClientsAsync(AuthorizationScope scope, IEnumerable<ProtectedClient> client)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveResourceAsync(AuthorizationScope scope, ProtectedResource resource)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<AuthorizationScope> RemoveResourcesAsync(AuthorizationScope scope, IEnumerable<ProtectedResource> resources)
        {
            throw new System.NotImplementedException();
        }
    }
}
