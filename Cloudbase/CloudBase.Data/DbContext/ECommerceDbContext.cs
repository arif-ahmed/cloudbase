﻿using Cloudbase.Entities.ECommerce;
using Cloudbase.Entities.SecurityModels;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class ECommerceDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            var tenant = httpContextAccessor.HttpContext.Items["TENANT"] as Tenant;

            if (tenant != null) tenant.DatabaseConnectionString = tenant.DatabaseConnectionString.Replace(@"\\", @"\");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CloudbaseDb;Integrated Security=SSPI;");
            //optionsBuilder.UseSqlServer(_tenant.DatabaseConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}