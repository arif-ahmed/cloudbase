using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cloudbase.Security.Models;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cloudbase.Security.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private SecurityDbContext _context;

        public ValuesController(SecurityDbContext context)
        {
            _context = context;
        }

        public IActionResult Get()
        {
            var count = _context.Users.Count();
            _context = DbContextFactory.Create("Data Source=.\\SQLEXPRESS;Initial Catalog=StudentSystemDb;Integrated Security=SSPI;");

            return Ok(_context.Students.Count());
        }
    }
}