using Casbin.Sam.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Casbin.Sam
{
    public static class ServiceCollectionExtension
    {
        public static CasbinSamBuilder AddCasbinSam(this IServiceCollection services)
        {
            return new CasbinSamBuilder(services);
        }
    }
}
