using Casbin.NET.Adapter.EFCore;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class CasbinSamRule : ICasbinRule<long>
    {
        public long Id { get; set; }
        public string ScopeId { get; set; }
        public string PType { get; set; }
        public string V0 { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; }
    }
}
