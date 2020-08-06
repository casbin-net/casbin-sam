using System.Threading.Tasks;

namespace Casbin.Sam.Management.Store
{
    public interface IRegisterStore<T>
    {
        public Task<T> AddRegisterAsync(T clientId);

        public ValueTask<T> UpdateRegisterAsync(string clientId, T register);

        public Task RemoveRegisterAsync(string clientId);

        public Task RemoveRegisterAsync(T register);
    }
}
