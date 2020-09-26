using System.Collections.Generic;
using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public readonly struct SamPolicy
    {
        public SamPolicy(IEnumerable<string> rule) :
            this(PermConstants.DefaultPolicyType, rule)
        {
        }

        public SamPolicy(string type, IEnumerable<string> rule)
        {
            Type = type;
            Rule = rule;
        }

        public string Type { get; }
        public IEnumerable<string> Rule { get; }
    }
}
