using System;
using Cloudbase.Entities.TenantModels;

namespace Cloudbase.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}