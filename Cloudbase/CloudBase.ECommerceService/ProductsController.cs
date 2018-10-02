
using System.Threading.Tasks;
using CloudBase.Data.DbContext;
using CloudBase.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.ECommerceService
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    { 
        public ECommerceDbContext Context { get; set; }

        public ProductsController(ECommerceDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Context.Categories.CountAsync());
        }
    }
}