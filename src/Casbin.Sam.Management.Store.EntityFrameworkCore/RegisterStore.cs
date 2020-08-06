using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class RegisterStore : IRegisterStore<Register>
    {
        public Task<Register> AddRegisterAsync(Register clientId)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Register> UpdateRegisterAsync(string clientId, Register register)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRegisterAsync(string clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRegisterAsync(Register register)
        {
            throw new System.NotImplementedException();
        }
    }
}
