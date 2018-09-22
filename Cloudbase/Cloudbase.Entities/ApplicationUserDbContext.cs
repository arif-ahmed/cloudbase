using System;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cloudbase.Entities
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser>
    {
        private Guid _tenantId;

        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {
            // _tenantId = tenantProvider.GetTenantId();
        }
    }
}