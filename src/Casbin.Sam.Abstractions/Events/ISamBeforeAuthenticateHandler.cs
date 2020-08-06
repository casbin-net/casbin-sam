using System.Threading.Tasks;

namespace Casbin.Sam.Abstractions.Events
{
    public interface ISamBeforeAuthenticateHandler
    {
        public Task HandleAsync(ISamAuthorizationContext context);
    }
}
