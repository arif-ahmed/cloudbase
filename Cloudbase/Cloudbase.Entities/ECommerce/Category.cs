

using System.Collections.Generic;

namespace Cloudbase.Entities.ECommerce
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}