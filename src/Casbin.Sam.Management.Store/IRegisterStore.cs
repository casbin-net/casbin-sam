using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Casbin.Sam.Management.Store
{
    public interface IRegisterStore<T>
    {
        public Task<IEnumerable<T>> GetRegistersAsync(string scopeId, bool track = false, CancellationToken cancellationToken = default);

        public ValueTask<T> GetRegisterAsync(string clientId, bool track = false, CancellationToken cancellationToken = default);

        public Task<T> AddRegisterAsync(T register, CancellationToken cancellationToken = default);

        public ValueTask<T> UpdateRegisterAsync(string clientId, T register, CancellationToken cancellationToken = default);

        public Task RemoveRegistersAsync(string scopeId, CancellationToken cancellationToken = default);

        public Task RemoveRegisterAsync(T register , CancellationToken cancellationToken = default);
    }
}
