using Casbin.AspNetCore.Authorization;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthorizationContext
    {
        public ISamAuthorizationData Data { get; set; }

        public ICasbinAuthorizationContext Context { get; set; }
    }
}
