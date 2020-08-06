using Casbin.Sam.Core;

namespace Casbin.Sam.Abstractions
{
    public interface ISamAuthorizationData
    {
        public string ScopeName { get; set; }
    }
}
