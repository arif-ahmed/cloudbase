using System;
using Cloudbase.Entities.TenantModels;
using Microsoft.AspNetCore.Identity;

namespace Cloudbase.Entities.SecurityModels
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}