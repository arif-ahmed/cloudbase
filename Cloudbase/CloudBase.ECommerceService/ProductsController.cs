
using System.Threading.Tasks;
using Cloudbase.Entities.BookShare;
using CloudBase.Data.DbContext;
using CloudBase.Infrastructure;
using CloudBase.Repository;
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
        public ProductRepository ProductRepository { get; set; }

        public ProductsController(ECommerceDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ProductRepository = new ProductRepository(Context);
            return Ok(await ProductRepository.Filter().CountAsync());
        }

        [UpdatePayloadProperties]
        [HttpPost]
        [Route("create")]
        public object Post(Author author)
        {
            return Ok(author);
        }
    }
}