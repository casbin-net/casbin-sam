using Casbin.AspNetCore.Authorization;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthorizationContextFactory
    {
        public ISamAuthorizationContext CreateContext(ICasbinAuthorizationContext context, ISamAuthorizationData data);
    }
}
