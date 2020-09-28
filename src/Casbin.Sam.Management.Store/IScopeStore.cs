using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Casbin.Sam.Management.Store
{
    public interface IScopeStore<T>
    {
        public Task<bool> HasScopeAsync(string scopeId, CancellationToken cancellationToken = default);

        public Task<IEnumerable<T>> GetScopesAsync(bool track = false, CancellationToken cancellationToken = default);

        public ValueTask<T> GetScopeAsync(string scopeId, bool track = false, CancellationToken cancellationToken = default);

        public Task<T> AddScopeAsync(T scope, CancellationToken cancellation = default);

        public ValueTask<T> UpdateScopeAsync(string scopeId, T scope, CancellationToken cancellationToken = default);

        public Task RemoveScopeAsync(T scopeId, CancellationToken cancellationToken = default);
    }
}
