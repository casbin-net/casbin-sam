using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public class CasbinModel
    {
        public CasbinModel(Model model, string versionToken)
        {
            Model = model;
            VersionToken = versionToken;
        }

        public Model Model { get; }

        public string VersionToken { get; }
    }
}
