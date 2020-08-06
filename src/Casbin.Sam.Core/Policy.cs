using System;
using System.Collections.Generic;
using NetCasbin.Model;

namespace Casbin.Sam.Core
{
    public readonly struct Policy
    {
        public Policy(IEnumerable<string> rule) :
            this(PermConstants.DefaultPolicyType, rule)
        {
        }

        public Policy(string type, IEnumerable<string> rule) :
            this(PermConstants.Section.PolicySection, type, rule)
        {
        }

        public Policy(string section, string type, IEnumerable<string> rule)
        {
            Section = section;
            Type = type;
            Rule = rule;
        }

        public string Section { get; }
        public string Type { get; }
        public IEnumerable<string> Rule { get; }
    }
}
