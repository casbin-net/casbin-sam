using System.Collections.Generic;

namespace Casbin.Sam.Core
{
    public class AuthorizationScope
    {
        public string? ScopeId { get; set; }
        public string? ScopeName { get; set; }
        public IEnumerable<ProtectedClient>? Clients {get; set; }
        public IEnumerable<ProtectedResource>? Resources { get; set; }
    }
}
