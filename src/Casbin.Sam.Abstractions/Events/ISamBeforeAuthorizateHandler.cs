using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions.Events
{
    public interface ISamBeforeAuthorizeHandler
    {
        public Task HandleAsync(ISamAuthorizationContext context);
    }
}
