
using Cloudbase.Entities;
using Cloudbase.Entities.SecurityModels;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class SecurityDbContext : IdentityDbContext<User>
    {
        private readonly Tenant Tenant;
        private DbContextOptionsBuilder _builder;

        public DbSet<Tenant> Tenants { get; set; }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }

        //public SecurityDbContext(DbContextOptions<SecurityDbContext> options, Tenant tenant) : base(options)
        //{
        //    Tenant = tenant;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (Tenant != null)
        //    {
        //        optionsBuilder.UseSqlServer(Tenant.DatabaseConnectionString);
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}