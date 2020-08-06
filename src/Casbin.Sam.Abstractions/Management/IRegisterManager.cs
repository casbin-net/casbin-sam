using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IRegisterManager<T>
    {
        public ValueTask<T> GetRegister(string clientId);

        public ValueTask<T> UpdateScopeAsync(T register, string scopeId);

        public ValueTask<T> UpdateVersionTokenAsync(T register, string versionToken);

        public Task RemoveRegisterAsync(T register);
    }
}
