using System.Linq;
using Cloudbase.Entities.TenantModels;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudBase.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Tenant Tenant { get; set; }

        public BaseController(TenantDbContext tenantDbContext, IHttpContextAccessor httpContextAccessor)
        {
            var origin = httpContextAccessor.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Origin").Value.ToString();
            Tenant = tenantDbContext.Tenants.FirstOrDefault(x => x.HostName == origin);
            if (Tenant != null) Tenant.DatabaseConnectionString = Tenant.DatabaseConnectionString.Replace(@"\\", @"\");
        }
    }
}