
using System.Linq;
using Cloudbase.Security.Models;
using CloudBase.Data;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cloudbase.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        public ValuesController(SecurityDbContext securityDbContext, TenantDbContext context, IHttpContextAccessor accessor) : base(securityDbContext, context, accessor)
        {
        }

        public IActionResult Get()
        {
            SecurityDbContext = DbContextFactory.Create(Tenant.DatabaseConnectionString);
            return Ok(SecurityDbContext.Students.Count());
        }
    }
}