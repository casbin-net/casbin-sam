using System.Collections.Generic;
using Casbin.Sam.Abstractions.Management;
using NetCasbin.Model;

namespace Casbin.Sam.Management
{
    public class CasbinModelCache : ICasbinModelCache<Model>
    {
        private readonly IDictionary<string, Model> _keyValuePairs
            = new Dictionary<string, Model>();

        public bool TryAddModel(string scopeId, Model model)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAddOrUpdateModel(string scopeId, Model model)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetModel(string scopeId, out Model model)
        {
            throw new System.NotImplementedException();
        }

        public bool TryRemoveModel(string scopeId, Model model)
        {
            throw new System.NotImplementedException();
        }
    }
}
