using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store;

namespace Casbin.Sam.Management
{
    public class SamScopeModelCacheHandler : ISamScopeModelCacheHandler<SamScopeModel>
    {
        private readonly IScopeStore<AuthorizationScope> _scopeStore;

        public SamScopeModelCacheHandler(IScopeStore<AuthorizationScope> scopeStore)
        {
            _scopeStore = scopeStore;
        }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public async Task<ISamScopeModelCache<SamScopeModel>> SetScopesCacheAsync(ISamScopeModelCache<SamScopeModel> cache)
        {
            var scopes = await _scopeStore.GetScopesAsync(cancellationToken: CancellationToken);
            var scopeList = scopes as List<AuthorizationScope> ?? scopes.ToList();
            foreach (var scope in scopeList)
            {
                // TODO : Need to Casbin.NET.Abstractions package
            }

            throw new NotImplementedException();
        }

        public Task<bool> TrySetScopeCacheAsync(ISamScopeModelCache<SamScopeModel> cache, out SamScopeModel scopeModel)
        {
            throw new NotImplementedException();
        }
    }
}
