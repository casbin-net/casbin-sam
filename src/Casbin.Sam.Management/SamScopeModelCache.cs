using System.Collections.Concurrent;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class SamScopeModelCache : ISamScopeModelCache<SamScopeModel>
    {
        private readonly ConcurrentDictionary<string, SamScopeModel> _casbinModels
            = new ConcurrentDictionary<string, SamScopeModel>();

        public bool HasModel(string scopeId)
        {
            return _casbinModels.ContainsKey(scopeId);
        }

        public bool TryGetModel(string scopeId, out SamScopeModel model)
        {
            return _casbinModels.TryGetValue(scopeId, out model);
        }

        public SamScopeModel AddOrUpdateModel(string scopeId, SamScopeModel model)
        {
            return _casbinModels.AddOrUpdate(scopeId, s => model,
                (s, old) => model);
        }

        public SamScopeModel GetOrAddModel(string scopeId, SamScopeModel model)
        {
            return _casbinModels.GetOrAdd(scopeId, s => model);
        }

        public bool TryRemoveModel(string scopeId)
        {
            return _casbinModels.TryRemove(scopeId, out _);
        }
    }
}
