using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace Cloudbase.Security.Controllers
{
    public class BaseController : Controller
    {
        protected SecurityDbContext DbContext { get; set; }

        public BaseController(SecurityDbContext context)
        {
            DbContext = context;
        }
    }
}
