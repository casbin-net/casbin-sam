using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthenticateService
    {
        public Task<bool> AuthenticateAsync(ISamAuthorizationContext context);
    }
}
