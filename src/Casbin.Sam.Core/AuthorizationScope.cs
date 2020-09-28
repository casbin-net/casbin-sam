using System.Collections.Generic;

namespace Casbin.Sam.Core
{
    public class AuthorizationScope
    {
        public string ScopeId { get; set; } = SamConstants.DefaultAuthorizationScopeId;
        public string? ScopeName { get; set; }
        public string? VersionToken { get; set; }
        public ICollection<ProtectedClient>? Clients {get; set; }
        public ICollection<ProtectedResource>? Resources { get; set; }
    }
}
