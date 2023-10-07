

using Shared.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Category:BaseEntity
    {
       
        public string Name { get; set; }
      
        public int? TopCategoryId { get; set; }
        public virtual Category TopCategory { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
