using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthorizationService
    {
        public Task<SamAuthorizationResult> AuthorizeAsync();
    }
}
