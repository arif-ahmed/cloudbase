

using System;
using System.Linq;
using Cloudbase.Entities;
using Cloudbase.Entities.TenantModels;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class TenantDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TenantDbContext(DbContextOptions<TenantsContext> options)
            : base(options)
        {
        }
    }
}