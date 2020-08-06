using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public class CasbinModel
    {
        public CasbinModel(Model model)
        {
            Model = model;
        }

        public Model Model { get; set; }

        public string? VersionToken { get; set; }
    }
}
