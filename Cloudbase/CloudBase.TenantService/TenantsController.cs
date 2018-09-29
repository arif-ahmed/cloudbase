using System.Threading.Tasks;
using Cloudbase.Entities.TenantModels;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CloudBase.TenantService
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        public TenantDbContext TenantDbContext { get; set; }

        public TenantsController(TenantDbContext tenantDbContext)
        {
            TenantDbContext = tenantDbContext;
        }

        public async Task<IActionResult> Get()
        {
            var tenants = await TenantDbContext.Tenants.ToListAsync();
            return Ok(tenants);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Tenant tenant)
        {
            await TenantDbContext.AddAsync(tenant);
            await TenantDbContext.SaveChangesAsync();
            return Ok(tenant);
        }
    }
}