﻿
using Cloudbase.Entities;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudBase.Data.DbContext
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Students { get; set; }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
            //_tenantId = tenantProvider.GetTenantId();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Playlist>().HasKey(e => e.Id);
        //    //modelBuilder.Entity<Song>().HasKey(e => e.Id);

        //    base.OnModelCreating(modelBuilder);
        //}

        //public Guid GetTenantId(string host)
        //{
        //    var tenant = Tenants.FirstOrDefault(t => t.Host == host);

        //    return tenant.Id;
        //}
    }
}