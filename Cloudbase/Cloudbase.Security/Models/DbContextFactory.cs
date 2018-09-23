using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cloudbase.Security.Models
{
    public static class DbContextFactory
    {
        public static Dictionary<string, string> ConnectionStrings { get; set; }

        public static void SetConnectionString(Dictionary<string, string> connStrs)
        {
            ConnectionStrings = connStrs;
        }

        public static SecurityDbContext Create(string connid)
        {
            if (!string.IsNullOrEmpty(connid))
            {
                var connStr = connid; //ConnectionStrings[connid];
                var optionsBuilder = new DbContextOptionsBuilder<SecurityDbContext>();
                optionsBuilder.UseSqlServer(connStr);
                return new SecurityDbContext(optionsBuilder.Options);
            }

            throw new ArgumentNullException(nameof(connid));
        }

        public static IDbContext Create(string connid)
        {
            if (!string.IsNullOrEmpty(connid))
            {
                var connStr = connid; //ConnectionStrings[connid];
                var optionsBuilder = new DbContextOptionsBuilder<SecurityDbContext>();
                optionsBuilder.UseSqlServer(connStr);
                return new SecurityDbContext(optionsBuilder.Options);
            }

            throw new ArgumentNullException(nameof(connid));
        }
    }
}
