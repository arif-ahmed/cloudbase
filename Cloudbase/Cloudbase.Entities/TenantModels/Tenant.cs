using System;

namespace Cloudbase.Entities.TenantModels
{
    public class Tenant
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Host { get; set; }

        public string DatabaseConnectionString { get; set; }
    }
}