using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthenticateService
    {
        public Task<bool> AuthenticateAsync(ISamAuthorizationContext context);
    }
}
