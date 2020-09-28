using System.Collections.Generic;
using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public class SamScopeModel
    {
        public SamScopeModel(string scopeId, Model model, string versionToken, AuthorizationScope authorizationScope)
        {
            ScopeId = scopeId;
            Model = model;
            VersionToken = versionToken;
            AuthorizationScope = authorizationScope;
        }

        public string ScopeId { get; }

        public Model Model { get; }

        public string VersionToken { get; set; }

        public AuthorizationScope AuthorizationScope { get; set; }

        public IDictionary<string, SamRegister>? SamRegisters { get; set; }
    }
}
