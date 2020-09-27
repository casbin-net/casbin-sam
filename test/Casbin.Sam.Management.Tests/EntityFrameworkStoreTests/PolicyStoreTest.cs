using System;
using System.Linq;
using System.Threading.Tasks;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store;
using Casbin.Sam.Management.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using NetCasbin.Model;
using Xunit;

namespace Casbin.Sam.Management.Tests.EntityFrameworkStoreTests
{
    public class PolicyStoreTest : IClassFixture<ServicesFixture>
    {
        private readonly IServiceProvider _servicesProvider;

        public PolicyStoreTest(ServicesFixture servicesFixture)
        {
            _servicesProvider = servicesFixture.ServiceProvider;
        }

        private IPolicyStore<SamPolicy> GetPolicyStore()
        {
            return _servicesProvider.CreateScope().ServiceProvider.GetRequiredService<IPolicyStore<SamPolicy>>();
        }

        [Fact]
        public async Task ShouldAddPolicy()
        {
            var store = GetPolicyStore();
            string scopeId = SamConstants.DefaultAuthorizationScopeId;
            const string policyType = PermConstants.DefaultPolicyType;
            var rule = new[] {"user1", "resource1", "GET"};

            await store.AddPolicyAsync(scopeId, new SamPolicy(policyType, rule));

            var policies = (await store.GetPoliciesAsync(scopeId, policyType)).ToArray();

            Assert.Equal(1, policies.Length);
            Assert.Equal(rule, policies.First().Rule);
            Assert.False(ReferenceEquals(rule, policies.First().Rule));
        }



    }
}
