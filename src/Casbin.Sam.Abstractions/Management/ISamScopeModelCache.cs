
namespace Casbin.Sam.Abstractions.Management
{
    public interface ISamScopeModelCache<T>
    {
        public bool HasModel(string scopeId);

        public bool TryGetModel(string scopeId, out T model);

        public T GetOrAddModel(string scopeId, T model);

        public T AddOrUpdateModel(string scopeId, T model);

        public bool TryRemoveModel(string scopeId);
    }
}
