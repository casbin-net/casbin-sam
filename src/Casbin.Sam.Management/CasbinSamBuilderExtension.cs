using Casbin.Sam.Abstractions;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Casbin.Sam.Management
{
    public static class CasbinSamBuilderExtension
    {
        public static CasbinSamBuilder AddManagement(this CasbinSamBuilder builder)
        {
            var services = builder.Services;
            services.TryAddScoped<IPolicyManager<SamPolicy>, PolicyManager>();
            services.TryAddScoped<IScopeManager<AuthorizationScope>, ScopeManager>();
            services.TryAddScoped<IRegisterManager<Register>, RegisterManager>();
            services.TryAddSingleton<IVersionTokenProvider<CasbinSamModel, string>, VersionTokenProvider>();
            services.TryAddSingleton<ICasbinSamModelCache<CasbinSamModel>, CasbinSamModelCache>();
            return builder;
        }
    }
}
