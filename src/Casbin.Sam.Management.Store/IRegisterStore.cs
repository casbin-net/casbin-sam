using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Casbin.Sam.Management.Store
{
    public interface IRegisterStore<T>
    {
        public Task<IEnumerable<T>> GetRegisterByScopeIdAsync(string scopeId, bool track = false, CancellationToken cancellationToken = default);

        public Task<T> GetRegisterByClientIdAsync(string clientId, bool track = false, CancellationToken cancellationToken = default);

        public Task<T> AddRegisterAsync(T register, CancellationToken cancellationToken = default);

        public ValueTask<T> UpdateRegisterAsync(string clientId, T register, CancellationToken cancellationToken = default);

        public Task RemoveRegisterByScopeIdAsync(string scopeId, CancellationToken cancellationToken = default);

        public Task RemoveRegisterByClientIdAsync(string clientId, CancellationToken cancellationToken = default);
    }
}
