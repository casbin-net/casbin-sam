using System;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class ScopeStore : IScopeStore<AuthorizationScope>
    {
        private readonly SamDbContext _samDbContext;
        private readonly StoreOptions _storeOptions;
        private readonly DbSet<AuthorizationScope> _scopes;

        private bool AutoSave => _storeOptions.AutoSave;

        public ScopeStore(SamDbContext samDbContext, IOptions<StoreOptions> storeOptions)
        {
            _samDbContext = samDbContext ?? throw new ArgumentNullException(nameof(samDbContext));
            _storeOptions = storeOptions.Value ?? throw new ArgumentNullException(nameof(storeOptions.Value));
            _scopes = _samDbContext.AuthorizationScopes ?? throw new ArgumentNullException(nameof(samDbContext));
        }

        public async Task<bool> HasScopeAsync(string scopeId, CancellationToken cancellationToken = default)
        {
            return await _scopes.AsNoTracking().AnyAsync(s =>
                string.Equals(s.ScopeId, scopeId), cancellationToken);
        }

        public async ValueTask<AuthorizationScope> GetScopeAsync(string scopeId, bool track = false, CancellationToken cancellationToken = default)
        {
            var scopes = _scopes.AsQueryable();

            if (track is false)
            {
                scopes = scopes.AsNoTracking();
            }

            return await scopes.FirstOrDefaultAsync(s => string.Equals(s.ScopeId, scopeId), cancellationToken: cancellationToken);
        }

        public async Task<AuthorizationScope> AddScopeAsync(AuthorizationScope scope, CancellationToken cancellationToken = default)
        {
            await _scopes.AddAsync(scope, cancellationToken);
            await TrySaveChanges(cancellationToken);
            return scope;
        }

        public async ValueTask<AuthorizationScope> UpdateScopeAsync(string scopeId, AuthorizationScope scope, CancellationToken cancellationToken = default)
        {
            scope.ScopeId = scopeId;
            _scopes.Update(scope);
            await TrySaveChanges(cancellationToken);
            return scope;
        }

        public async Task RemoveScopeAsync(string scopeId, CancellationToken cancellationToken = default)
        {
            var scope = await GetScopeAsync(scopeId, cancellationToken: cancellationToken);
            _scopes.Remove(scope);
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
