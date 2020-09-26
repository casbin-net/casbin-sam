using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public class CasbinSamModel
    {
        public CasbinSamModel(string scopeId, Model model, string versionToken)
        {
            Model = model;
            VersionToken = versionToken;
            ScopeId = scopeId;
        }

        public string ScopeId { get; }

        public Model Model { get; }

        public string VersionToken { get; set; }
    }
}
