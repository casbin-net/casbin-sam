﻿using System.Data;
using Casbin.Sam.Core;
using Microsoft.EntityFrameworkCore;

namespace Casbin.Sam.Management.Store.EntityFrameworkCore
{
    public class SamDbContext : DbContext
    {
        public SamDbContext(DbContextOptions<SamDbContext> options)
            : base(options)
        {

        }

        public DbSet<SamRegister> Registers { get; set; }
        public DbSet<CasbinSamRule> CasbinSamRule { get; set; }
        public DbSet<AuthorizationScope> AuthorizationScopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamRegister>().HasKey(register => new { register.ScopeId, register.ClientId });

            modelBuilder.Entity<CasbinSamRule>().HasKey(rule => rule.Id);

            modelBuilder.Entity<AuthorizationScope>(scope =>
            {
                scope.HasData(new AuthorizationScope
                {
                    ScopeId = SamConstants.DefaultAuthorizationScopeId,
                    ScopeName = SamConstants.DefaultAuthorizationScopeId
                });
                scope.HasKey(s => s.ScopeId);
            });

            modelBuilder.Entity<ProtectedClient>().HasKey(client => client.ClientId);
            modelBuilder.Entity<ProtectedResource>().HasKey(resource => resource.ResourceId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
