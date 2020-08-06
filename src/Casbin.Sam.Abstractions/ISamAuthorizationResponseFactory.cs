using System.Threading.Tasks;
using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthorizationResponseFactory<T>
    {
        public Task<T> CreateResponseAsync(SamAuthorizationResult result);
    }
}
