using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IScopeManager<T>
    {
        public ValueTask<T> GetScopeAsync(string scopeId);

        public ValueTask<T> RemoveScopeAsync(string scopeId);

        public ValueTask<T> RemoveScopeAsync(T scope);

        public ValueTask<T> AddClientAsync(T scope, ProtectedClient client);

        public ValueTask<T> AddResourceAsync(T scope, ProtectedResource resource);

        public ValueTask<T> AddClientsAsync(T scope, IEnumerable<ProtectedClient> client);

        public ValueTask<T> AddResourcesAsync(T scope, IEnumerable<ProtectedResource> resources);

        public ValueTask<T> RemoveClientAsync(T scope, ProtectedClient client);

        public ValueTask<T> RemoveResourceAsync(T scope, ProtectedResource resource);

        public ValueTask<T> RemoveClientsAsync(T scope, IEnumerable<ProtectedClient> client);

        public ValueTask<T> RemoveResourcesAsync(T scope, IEnumerable<ProtectedResource> resources);
    }
}
