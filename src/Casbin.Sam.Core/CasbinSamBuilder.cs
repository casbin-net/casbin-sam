using Microsoft.Extensions.DependencyInjection;

namespace Casbin.Sam.Core
{
    public class CasbinSamBuilder
    {
        public CasbinSamBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
