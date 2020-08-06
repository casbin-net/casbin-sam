namespace Casbin.Sam.Abstractions.Management
{
    public interface ICasbinModelCache<T>
    {
        public bool TryAddModel(string scopeId, T model);

        public bool TryGetModel(string scopeId, out T model);

        public bool TryAddOrUpdateModel(string scopeId, T model);

        public bool TryRemoveModel(string scopeId, T model);
    }
}
