using CloudBase.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CloudBase.Data.DbContextFactories
{
    public class ECommerceDbContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ECommerceDbContext>();
            builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=EShopDb;Integrated Security=SSPI;");
            return new ECommerceDbContext(builder.Options);
        }
    }
}