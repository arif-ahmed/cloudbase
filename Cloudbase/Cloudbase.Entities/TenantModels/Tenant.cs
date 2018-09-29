using System;

namespace Cloudbase.Entities.TenantModels
{
    public class Tenant : Entity
    {
        public string Name { get; set; }

        public string HostName { get; set; }

        public string DatabaseConnectionString { get; set; }
    }
}