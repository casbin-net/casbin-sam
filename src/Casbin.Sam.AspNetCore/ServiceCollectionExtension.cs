using Casbin.Sam.Core;
using Casbin.Sam.Management;
using Microsoft.Extensions.DependencyInjection;

namespace Casbin.Sam.AspNetCore
{
    public static class ServiceCollectionExtension
    {
        public static CasbinSamBuilder AddCasbinSamAuthorization(this IServiceCollection services)
        {
            var builder = services.AddCasbinSam();
            builder.AddManagement();
            return builder;
        }
    }
}
