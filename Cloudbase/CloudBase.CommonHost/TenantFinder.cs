
using System.Linq;
using System.Threading.Tasks;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.CommonHost
{
    public class TenantFinder
    {
        private readonly RequestDelegate _next;
        private TenantDbContext _tenantDbContext;
        public TenantFinder(RequestDelegate next)
        {
            _next = next;
            //_tenantDbContext = tenantDbContext;
        }

        public async Task Invoke(HttpContext context)
        { 
            _tenantDbContext = new TenantDbContext(new DbContextOptions<TenantDbContext>());
            context.Items["TENANT"] =
                await _tenantDbContext.Tenants.FirstOrDefaultAsync(tenant => tenant.HostName == "www.ecommerce.com");
            await _next.Invoke(context);
        }
    }

    public static class TenantFinderExtension
    {
        public static IApplicationBuilder UseTenantFinder(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantFinder>();
            return app;
        }
    }
}