
using Cloudbase.Entities;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly Tenant Tenant;
        private DbContextOptionsBuilder _builder;

        public DbSet<Student> Students { get; set; }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options, Tenant tenant) : base(options)
        {
            Tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI;");
                        base.OnConfiguring(optionsBuilder);*/

            if (Tenant != null)
            {
                optionsBuilder.UseSqlServer(Tenant.DatabaseConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}