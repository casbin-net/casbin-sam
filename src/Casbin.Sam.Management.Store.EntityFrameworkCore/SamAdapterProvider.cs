namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class SamAdapterProvider
    {
        private readonly SamDbContext _samDbContext;

        public SamAdapterProvider(SamDbContext samDbContext)
        {
            _samDbContext = samDbContext;
        }

        public SamAdapter GetAdapter(string scopeId)
        {
            return new SamAdapter(_samDbContext, scopeId);
        }
    }
}
