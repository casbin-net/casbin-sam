using System.Collections.Concurrent;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;

namespace Casbin.Sam.Management
{
    public class CasbinSamModelCache : ICasbinSamModelCache<CasbinSamModel>
    {
        private readonly ConcurrentDictionary<string, CasbinSamModel> _casbinModels
            = new ConcurrentDictionary<string, CasbinSamModel>();

        public bool TryGetModel(string scopeId, out CasbinSamModel model)
        {
            return _casbinModels.TryGetValue(scopeId, out model);
        }

        public CasbinSamModel AddOrUpdateModel(string scopeId, CasbinSamModel model)
        {
            return _casbinModels.AddOrUpdate(scopeId, s => model,
                (s, old) => model);
        }

        public CasbinSamModel GetOrAddModel(string scopeId, CasbinSamModel model)
        {
            return _casbinModels.GetOrAdd(scopeId, s => model);
        }

        public bool TryRemoveModel(string scopeId)
        {
            return _casbinModels.TryRemove(scopeId, out _);
        }
    }
}
