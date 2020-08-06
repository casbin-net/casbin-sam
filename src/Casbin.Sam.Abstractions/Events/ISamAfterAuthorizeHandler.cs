using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions.Events
{
    public interface ISamAfterAuthorizeHandler
    {
        public Task HandleAsync(SamAuthorizationResult result);
    }
}
