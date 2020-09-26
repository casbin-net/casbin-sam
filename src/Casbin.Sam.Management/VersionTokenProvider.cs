using System.Threading.Tasks;
using Casbin.Sam.Abstractions;
using Casbin.Sam.Core;
using Casbin.Sam.Core.Services;

namespace Casbin.Sam.Management
{
    public class VersionTokenProvider : IVersionTokenProvider<CasbinSamModel, string>
    {
        public Task<string> GenerateVersionTokenAsync()
        {
            string token = VersionTokenService.GenerateRandomVersionToken();
            return Task.FromResult(token);
        }

        public Task<string> GenerateVersionTokenAsync(CasbinSamModel source)
        {
            return GenerateVersionTokenAsync();
        }
    }
}
