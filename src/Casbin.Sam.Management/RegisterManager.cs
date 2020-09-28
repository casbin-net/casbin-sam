using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Casbin.Sam.Abstractions.Management;
using Casbin.Sam.Core;
using Casbin.Sam.Management.Store;

namespace Casbin.Sam.Management
{
    public class RegisterManager : IRegisterManager<SamRegister>
    {
        private readonly IRegisterStore<SamRegister> _registerStore;
        private readonly ISamScopeModelCache<SamScopeModel> _samScopeModelCache;

        public RegisterManager(IRegisterStore<SamRegister> registerStore,
            ISamScopeModelCache<SamScopeModel> samScopeModelCache)
        {
            _registerStore = registerStore;
            _samScopeModelCache = samScopeModelCache;
        }

        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        public async ValueTask<IEnumerable<SamRegister>> GetRegisters(string scopeId, bool track = false)
        {
            if (_samScopeModelCache.TryGetModel(scopeId, out var model) is false)
            {
                throw new ApplicationException($"Can not find the special scope {scopeId}");
            }

            if (model.SamRegisters is not null)
            {
                return model.SamRegisters.Values;
            }

            var registers = await _registerStore.GetRegistersAsync(scopeId, track, CancellationToken);
            var registersArray = registers as SamRegister[] ?? registers.ToArray(); 
            model.SamRegisters = registersArray.ToDictionary(r => r.ClientId);
            return registersArray;
        }

        public ValueTask<SamRegister> GetRegister(string clientId, bool track = false)
        {
            return _registerStore.GetRegisterAsync(clientId, track, CancellationToken);
        }

        public async Task<SamRegister> AddRegister(string scopeId, SamRegister register)
        {
            if (string.IsNullOrEmpty(register.ClientId))
            {
                throw new ArgumentNullException(nameof(register.ClientId));
            }

            if (_samScopeModelCache.TryGetModel(scopeId, out var model) is false)
            {
                throw new ApplicationException($"Can not find the special scope {scopeId}");
            }

            if (model.SamRegisters is null)
            {
                await SetRegistersOnScopeModelAsync(scopeId, model);
            }

            register.ScopeId = scopeId;
            register = await _registerStore.AddRegisterAsync(register, CancellationToken);

            if (model.SamRegisters.TryAdd(register.ClientId, register) is false)
            {
                throw new ApplicationException($"Can not add the register to special scope {scopeId}");
            }

            return register;
        }

        public async Task<SamRegister> RemoveRegisterAsync(SamRegister register)
        {
            if (string.IsNullOrEmpty(register.ClientId))
            {
                throw new ArgumentNullException(nameof(register.ClientId));
            }

            if (_samScopeModelCache.TryGetModel(register.ScopeId, out var model) is false)
            {
                throw new ApplicationException($"Can not find the special scope {register.ScopeId}");
            }

            if (model.SamRegisters is null)
            {
                await SetRegistersOnScopeModelAsync(register.ScopeId, model);
            }

            await _registerStore.RemoveRegisterAsync(register, CancellationToken);

            model.SamRegisters?.Remove(register.ClientId);
            return register;
        }

        public async ValueTask<SamRegister> UpdateScopeAsync(SamRegister register, string scopeId)
        {
            string oldScopeId = register.ScopeId;
            register.ScopeId = scopeId;
            return await UpdateAsync(oldScopeId, register);
        }

        public async ValueTask<SamRegister> UpdateVersionTokenAsync(SamRegister register, string versionToken)
        {
            register.VersionToken = versionToken;
            return await UpdateAsync(register.ScopeId, register);
        }

        protected virtual async ValueTask<SamRegister> UpdateAsync(string oldScopeId, SamRegister newRegister)
        {
            if (string.IsNullOrEmpty(oldScopeId))
            {
                throw new ArgumentNullException(nameof(oldScopeId));
            }

            bool sameScopeUpdate = oldScopeId == newRegister.ScopeId;

            if (_samScopeModelCache.TryGetModel(oldScopeId, out var model) is false)
            {
                throw new ApplicationException($"Can not find the special scope {oldScopeId}");
            }

            if (sameScopeUpdate)
            {
                if (model.SamRegisters is null)
                {
                    await SetRegistersOnScopeModelAsync(oldScopeId, model);
                }

                newRegister = await _registerStore.UpdateRegisterAsync(newRegister.ScopeId, newRegister, CancellationToken);

                if (model.SamRegisters is not null)
                {
                    model.SamRegisters[newRegister.ClientId] = newRegister;
                }
            }
            else
            {
                if (_samScopeModelCache.TryGetModel(newRegister.ScopeId, out var newModel) is false)
                {
                    throw new ApplicationException($"Can not find the special scope {newRegister.ScopeId}");
                }

                newRegister = await _registerStore.UpdateRegisterAsync(newRegister.ScopeId, newRegister, CancellationToken);

                // if update success, It will clean the cache.
                model.SamRegisters = null;
                newModel.SamRegisters = null;
            }

            return newRegister;
        }

        private async Task SetRegistersOnScopeModelAsync(string scopeId, SamScopeModel model)
        {
            var registers = await _registerStore.GetRegistersAsync(scopeId, false, CancellationToken);
            model.SamRegisters = registers.ToDictionary(r => r.ClientId);
        }
    }
}
