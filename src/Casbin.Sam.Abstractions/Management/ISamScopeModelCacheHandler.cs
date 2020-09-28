using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions.Management
{
    public interface ISamScopeModelCacheHandler<T>
    {
        public Task<ISamScopeModelCache<T>> SetScopesCacheAsync(ISamScopeModelCache<T> cache);

        public Task<bool> TrySetScopeCacheAsync(ISamScopeModelCache<T> cache, out T scopeModel);
    }
}
