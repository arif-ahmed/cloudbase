

using Cloudbase.Entities.TenantModels;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class TenantDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CloudbaseDb;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>().ToTable("Tenants");
            //modelBuilder.Entity<User>().ToTable("Users");
            // add your own confguration here
            base.OnModelCreating(modelBuilder);
        }
    }
}