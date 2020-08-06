using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Events
{
    public interface ISamAfterAuthenticateHandler
    {
        public Task HandleAsync(SamAuthorizationResult result);
    }
}
