using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class RegisterStore : IRegisterStore<SamRegister>
    {
        private readonly SamDbContext _samDbContext;
        private readonly StoreOptions _storeOptions;
        private readonly DbSet<SamRegister> _registers;

        private bool AutoSave => _storeOptions.AutoSave;

        public RegisterStore(SamDbContext samDbContext, IOptions<StoreOptions> storeOptions)
        {
            _samDbContext = samDbContext ?? throw new ArgumentNullException(nameof(samDbContext));
            _storeOptions = storeOptions.Value ?? throw new ArgumentNullException(nameof(storeOptions.Value));
            _registers = _samDbContext.Registers ?? throw new ArgumentNullException(nameof(samDbContext));
        }

        public async Task<IEnumerable<SamRegister>> GetRegistersAsync(string scopeId, bool track = false,
            CancellationToken cancellationToken = default)
        {
            var registers = _registers.Where(r => string.Equals(r.ScopeId, scopeId));

            if (track is false)
            {
                registers = registers.AsNoTracking();
            }

            return await registers.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async ValueTask<SamRegister> GetRegisterAsync(string clientId, bool track = false,
            CancellationToken cancellationToken = default)
        {
            var registers = _registers.AsQueryable();

            if (track is false)
            {
                registers = registers.AsNoTracking();
            }

            return await registers.FirstOrDefaultAsync(r =>
                string.Equals(r.ClientId, clientId), cancellationToken);
        }

        public async Task<SamRegister> AddRegisterAsync(SamRegister register, CancellationToken cancellationToken = default)
        {
            await _registers.AddAsync(register, cancellationToken);
            await TrySaveChanges(cancellationToken);
            return register;
        }

        public async ValueTask<SamRegister> UpdateRegisterAsync(string clientId, SamRegister register, CancellationToken cancellationToken = default)
        {
            register.ClientId = clientId;
            _registers.Update(register);
            await TrySaveChanges(cancellationToken);
            return register;
        }

        public async Task RemoveRegistersAsync(string scopeId, CancellationToken cancellationToken = default)
        {
            var registers = await GetRegistersAsync(scopeId, cancellationToken: cancellationToken);
            _registers.RemoveRange(registers);
            await TrySaveChanges(cancellationToken);
        }

        public async Task RemoveRegisterAsync(SamRegister register, CancellationToken cancellationToken = default)
        {
            _registers.Remove(register);
            await TrySaveChanges(cancellationToken);
        }

        private async Task<bool> TrySaveChanges(CancellationToken cancellationToken = default)
        {
            if (AutoSave)
            {
                return await _samDbContext.SaveChangesAsync(cancellationToken) > 0;
            }

            return await Task.FromResult(false);
        }
    }
}
