using System.Collections.Generic;
using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Management
{
    public interface IRegisterManager<T>
    {
        public ValueTask<IEnumerable<SamRegister>> GetRegisters(string scopeId, bool track = false);

        public ValueTask<T> GetRegister(string clientId, bool track);

        public Task<T> AddRegister(string scopeId, T register);

        public ValueTask<T> UpdateScopeAsync(T register, string scopeId);

        public ValueTask<T> UpdateVersionTokenAsync(T register, string versionToken);

        public Task<T> RemoveRegisterAsync(T register);
    }
}
