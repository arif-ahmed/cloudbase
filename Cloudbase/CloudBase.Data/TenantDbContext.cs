
using Cloudbase.Entities.TenantModels;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data
{
    public class TenantDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Tenantdb;Integrated Security=SSPI;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}