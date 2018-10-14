using Cloudbase.Entities.ECommerce;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CloudBase.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}