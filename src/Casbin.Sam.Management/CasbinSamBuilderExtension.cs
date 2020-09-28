using Casbin.Sam.Abstractions;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
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
            services.TryAddScoped<IRegisterManager<SamRegister>, RegisterManager>();
            services.TryAddSingleton<IVersionTokenProvider<SamScopeModel, string>, VersionTokenProvider>();
            services.TryAddSingleton<ISamScopeModelCache<SamScopeModel>, SamScopeModelCache>();
            return builder;
        }
    }
}
