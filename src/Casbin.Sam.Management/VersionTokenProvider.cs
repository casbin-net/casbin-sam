using System.Threading.Tasks;
using Casbin.Sam.Abstractions;
using Casbin.Sam.Core;
using Casbin.Sam.Core.Services;

namespace Casbin.Sam.Management
{
    public class VersionTokenProvider : IVersionTokenProvider<CasbinModel, string>
    {
        public Task<string> GenerateVersionTokenAsync(CasbinModel source)
        {
            string token = VersionTokenService.GenerateRandomVersionToken();
            return Task.FromResult(token);
        }
    }
}
