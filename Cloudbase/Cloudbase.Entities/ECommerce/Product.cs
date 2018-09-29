
using System;

namespace Cloudbase.Entities.ECommerce
{
    public class Product : Entity
    {
        public string Name { get; set; }

        public Guid CategoryGuid { get; set; }

        public Category Category { get; set; }
    }
}