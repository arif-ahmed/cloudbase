using System;

namespace Cloudbase.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}