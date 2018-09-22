using System;
using System.Linq;
using Cloudbase.Entities.TenantModels;
using Microsoft.EntityFrameworkCore;

namespace Cloudbase.Entities
{
    public class TenantsContext : DbContext
    {
        private DbSet<Tenant> Tenants { get; set; }

        public TenantsContext(DbContextOptions<TenantsContext> options)
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