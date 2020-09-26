using System.Collections.Generic;
using System.Linq;
using Casbin.NET.Adapter.EFCore;
using Microsoft.EntityFrameworkCore;
using NetCasbin.Model;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class SamAdapter : CasbinDbAdapter<long, CasbinSamRule, SamDbContext>
    {
        public string ScopeId { get; }

        public SamAdapter(SamDbContext context, string scopeId) : base(context)
        {
            ScopeId = scopeId;
        }

        protected override DbSet<CasbinSamRule> GetCasbinRuleDbSet(SamDbContext dbContext)
        {
            return dbContext.CasbinSamRule;
        }

        protected override IQueryable<CasbinSamRule> OnLoadPolicy(Model model, IQueryable<CasbinSamRule> casbinRules)
        {
            return casbinRules.Where(r => string.Equals(r.ScopeId, ScopeId));
        }

        protected override IEnumerable<CasbinSamRule> OnSavePolicy(Model model, IEnumerable<CasbinSamRule> casbinSamRules)
        {
            IEnumerable<CasbinSamRule> casbinSamRuleArray = casbinSamRules as CasbinSamRule[] ?? casbinSamRules.ToArray();

            foreach (var casbinSamRule in casbinSamRuleArray)
            {
                casbinSamRule.ScopeId = ScopeId;
            }

            return casbinSamRuleArray;
        }

        protected override CasbinSamRule OnAddPolicy(string section, string policyType, IEnumerable<string> rule, CasbinSamRule casbinSamRules)
        {
            casbinSamRules.ScopeId = ScopeId;
            return casbinSamRules;
        }

        protected override IQueryable<CasbinSamRule> OnRemoveFilteredPolicy(string section, string policyType, int fieldIndex, string[] fieldValues, IQueryable<CasbinSamRule> casbinRules)
        {
            return casbinRules.Where(r => string.Equals(r.ScopeId, ScopeId));
        }
    }
}
