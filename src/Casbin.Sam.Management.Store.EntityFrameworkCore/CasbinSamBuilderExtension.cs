using System;
using Casbin.Sam.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public static class CasbinSamBuilderExtension
    {
        public static CasbinSamBuilder AddEntityFrameworkStores<TContext>(this CasbinSamBuilder builder, Action<StoreOptions> optionAction)
            where TContext : SamDbContext
        {
            var services = builder.Services;
            services.Configure(optionAction);
            builder.AddEntityFrameworkStores<TContext>();
            return builder;
        }

        public static CasbinSamBuilder AddEntityFrameworkStores<TContext>(this CasbinSamBuilder builder)
            where TContext : SamDbContext
        {
            var services = builder.Services;
            services.TryAddScoped<IPolicyStore<SamPolicy>, PolicyStore>();
            services.TryAddScoped<IRegisterStore<SamRegister>, RegisterStore>();
            services.TryAddScoped<IScopeStore<AuthorizationScope>, ScopeStore>();
            services.TryAddSingleton<SamAdapterProvider>();
            return builder;
        }
    }
}
