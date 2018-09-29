

using System.Linq;
using System.Threading.Tasks;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudBase.ECommerceService
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ECommerceDbContext Context { get; set; }

        public ProductsController(ECommerceDbContext context)
        {
            Context = context;
        }

        [Authorize]
        [HttpGet]
        //[Route("list")]
        public async Task<IActionResult> Get()
        {
            return Ok("Product List");
        }
    }
}