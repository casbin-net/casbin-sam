using System.Threading.Tasks;

namespace Casbin.Sam.Management.Store
{
    public interface IScopeStore<T>
    {
        public ValueTask<T> GetScopeAsync(string scopeId);

        public Task<T> AddScopeAsync(T scope);

        public ValueTask<T> UpdateScopeAsync(string scopeId, T scope);

        public Task RemoveScopeAsync(string scopeId);
    }
}
