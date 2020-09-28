using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store;

namespace Casbin.Sam.Management
{
    public class ScopeManager : IScopeManager<AuthorizationScope>
    {
        private readonly IScopeStore<AuthorizationScope> _scopeStore;
        private readonly ISamScopeModelCache<SamScopeModel> _samScopeModelCache;

        public ScopeManager(IScopeStore<AuthorizationScope> scopeStore, ISamScopeModelCache<SamScopeModel> samScopeModelCache)
        {
            _scopeStore = scopeStore ?? throw new ArgumentNullException(nameof(scopeStore));
            _samScopeModelCache = samScopeModelCache;
        }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public Task<bool> HasScopeAsync(string scopeId)
        {
            return _scopeStore.HasScopeAsync(scopeId, CancellationToken);
        }

        public Task<AuthorizationScope> CreateScopeAsync(string scopeId, string scopeName, ICollection<ProtectedClient> clients = default, ICollection<ProtectedResource> resources = default)
        {
            var scope = new AuthorizationScope
            {
                ScopeId = scopeId,
                ScopeName = scopeName,
                Clients = clients,
                Resources = resources
            };
            return _scopeStore.AddScopeAsync(scope, CancellationToken);
        }

        public Task<IEnumerable<AuthorizationScope>> GetScopesAsync(bool track = false)
        {
            return _scopeStore.GetScopesAsync(track, CancellationToken);
        }

        public ValueTask<AuthorizationScope> GetScopeAsync(string scopeId, bool track = false)
        {
            return _scopeStore.GetScopeAsync(scopeId, track, CancellationToken);
        }

        public async ValueTask<AuthorizationScope> RemoveScopeAsync(string scopeId)
        {
            var scope = await GetScopeAsync(scopeId);
            return await RemoveScopeAsync(scope);
        }

        public async ValueTask<AuthorizationScope> RemoveScopeAsync(AuthorizationScope scope)
        {
            await _scopeStore.RemoveScopeAsync(scope, CancellationToken);
            return scope;
        }

        public async Task<AuthorizationScope> AddClientAsync(AuthorizationScope scope, ProtectedClient client)
        {
            CheckClientIsNull(scope);
            scope.Clients?.Add(client);
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async Task<AuthorizationScope> AddResourceAsync(AuthorizationScope scope, ProtectedResource resource)
        {
            CheckResourceIsNull(scope);
            scope.Resources?.Add(resource);
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async Task<AuthorizationScope> AddClientsAsync(AuthorizationScope scope, IEnumerable<ProtectedClient> clients)
        {
            CheckResourceIsNull(scope);
            if (scope.Clients is List<ProtectedClient> list)
            {
                list.AddRange(clients);
            }
            else
            {
                foreach (var client in clients)
                {
                    scope.Clients?.Add(client);
                }
            }
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async Task<AuthorizationScope> AddResourcesAsync(AuthorizationScope scope, IEnumerable<ProtectedResource> resources)
        {
            CheckResourceIsNull(scope);
            if (scope.Resources is List<ProtectedResource> list)
            {
                list.AddRange(resources);
            }
            else
            {
                foreach (var resource in resources)
                {
                    scope.Resources?.Add(resource);
                }
            }
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async ValueTask<AuthorizationScope> RemoveClientAsync(AuthorizationScope scope, ProtectedClient client)
        {
            CheckClientIsNull(scope);
            scope.Clients?.Remove(client);
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async ValueTask<AuthorizationScope> RemoveResourceAsync(AuthorizationScope scope, ProtectedResource resource)
        {
            CheckResourceIsNull(scope);
            scope.Resources?.Remove(resource);
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async ValueTask<AuthorizationScope> RemoveClientsAsync(AuthorizationScope scope, IEnumerable<ProtectedClient> clients)
        {
            CheckClientIsNull(scope);
            foreach (var client in clients)
            {
                scope.Clients?.Remove(client);
            }
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        public async ValueTask<AuthorizationScope> RemoveResourcesAsync(AuthorizationScope scope, IEnumerable<ProtectedResource> resources)
        {
            CheckResourceIsNull(scope);
            foreach (var resource in resources)
            {
                scope.Resources?.Remove(resource);
            }
            return await _scopeStore.UpdateScopeAsync(scope.ScopeId, scope, CancellationToken);
        }

        protected virtual void CheckClientIsNull(AuthorizationScope scope)
        {
            scope.Clients ??= new List<ProtectedClient>();
        }

        protected virtual void CheckResourceIsNull(AuthorizationScope scope)
        {
            scope.Resources ??= new List<ProtectedResource>();
        }
    }
}
