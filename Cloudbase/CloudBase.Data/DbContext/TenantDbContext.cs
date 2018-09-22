

using System;
using System.Linq;
using Cloudbase.Entities;
using Cloudbase.Entities.TenantModels;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class TenantDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private DbSet<Tenant> Tenants { get; set; }

        public TenantDbContext(DbContextOptions<TenantsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().HasKey(e => e.Id);
        }

        public Guid GetTenantId(string host)
        {
            var tenant = Tenants.FirstOrDefault(t => t.Host == host);

            return tenant.Id;
        }
    }
}