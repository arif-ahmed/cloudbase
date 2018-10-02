using System;

namespace Cloudbase.Entities.ECommerce
{
    public class SubCategory : Entity
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}