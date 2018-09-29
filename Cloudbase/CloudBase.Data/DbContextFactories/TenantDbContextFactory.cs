using System;
using CloudBase.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CloudBase.Data.DbContextFactories
{
    class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TenantDbContext>();
            builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CloudbaseDb;Integrated Security=SSPI;");
            return new TenantDbContext(builder.Options);
        }
    }
}
