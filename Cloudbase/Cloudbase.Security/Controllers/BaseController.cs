
using System.Linq;
using Cloudbase.Entities.TenantModels;
using CloudBase.Data;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cloudbase.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Tenant Tenant { get; set; }
        public TenantDbContext DbContext { get; set; }
        public SecurityDbContext SecurityDbContext { get; set; }

        public BaseController(SecurityDbContext securityDbContext, TenantDbContext context, IHttpContextAccessor accessor)
        {
            SecurityDbContext = securityDbContext;
            DbContext = context;
            var origin = accessor.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Origin").Value.ToString();
            Tenant = context.Tenants.FirstOrDefault(x => x.Host == origin);
        }
    }
}