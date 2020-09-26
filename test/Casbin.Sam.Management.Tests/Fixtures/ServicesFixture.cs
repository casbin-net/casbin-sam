using System;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCasbin;
using NetCasbin.Model;

namespace Casbin.Sam.Management.Tests.Fixtures
{
    public class ServicesFixture
    {
        public ServicesFixture()
        {
            var builder = new ServiceCollection().AddCasbinSam();
            builder.Services.AddDbContext<SamDbContext>(configure =>
            {
                configure.UseSqlite("Data Source=casbin_sam_test.db");
            });

            builder.AddManagement()
                .AddEntityFrameworkStores<SamDbContext>();

            ServiceProvider = builder.Services.BuildServiceProvider();

            using var scope = ServiceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<SamDbContext>().Database.EnsureCreated();

            var modelCache = ServiceProvider.GetRequiredService<ICasbinSamModelCache<CasbinSamModel>>();
            var scopeId = SamConstants.DefaultAuthorizationScopeId;
            modelCache.AddOrUpdateModel(scopeId,
                new CasbinSamModel(scopeId, CoreEnforcer.NewModel("Examples/store_test_model.conf", null), string.Empty));
        }

        public IServiceProvider ServiceProvider { get; }
    }
}
