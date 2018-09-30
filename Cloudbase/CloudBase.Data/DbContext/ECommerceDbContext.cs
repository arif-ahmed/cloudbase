using Cloudbase.Entities.ECommerce;
using Cloudbase.Entities.SecurityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class ECommerceDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}