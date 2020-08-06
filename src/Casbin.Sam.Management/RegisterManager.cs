using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class RegisterManager : IRegisterManager<Register>
    {

        public ValueTask<Register> GetRegister(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRegisterAsync(Register register)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Register> UpdateScopeAsync(Register register, string scopeId)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Register> UpdateVersionTokenAsync(Register register, string versionToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
