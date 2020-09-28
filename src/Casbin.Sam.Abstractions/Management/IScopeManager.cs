using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IScopeManager<T>
    {
        public Task<bool> HasScopeAsync(string scopeId);

        public Task<T> CreateScopeAsync(string scopeId, string scopeName, ICollection<ProtectedClient> clients = default, ICollection<ProtectedResource> resources = default);

        public Task<IEnumerable<T>> GetScopesAsync(bool track = false);

        public ValueTask<T> GetScopeAsync(string scopeId, bool track = false);

        public ValueTask<T> RemoveScopeAsync(string scopeId);

        public ValueTask<T> RemoveScopeAsync(T scope);

        public Task<T> AddClientAsync(T scope, ProtectedClient client);

        public Task<T> AddResourceAsync(T scope, ProtectedResource resource);

        public Task<T> AddClientsAsync(T scope, IEnumerable<ProtectedClient> client);

        public Task<T> AddResourcesAsync(T scope, IEnumerable<ProtectedResource> resources);

        public ValueTask<T> RemoveClientAsync(T scope, ProtectedClient client);

        public ValueTask<T> RemoveResourceAsync(T scope, ProtectedResource resource);

        public ValueTask<T> RemoveClientsAsync(T scope, IEnumerable<ProtectedClient> client);

        public ValueTask<T> RemoveResourcesAsync(T scope, IEnumerable<ProtectedResource> resources);
    }
}
