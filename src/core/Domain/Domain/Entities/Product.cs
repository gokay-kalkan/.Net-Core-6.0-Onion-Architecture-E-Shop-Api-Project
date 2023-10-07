

using Microsoft.AspNetCore.Http;
using Shared.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public string SmartUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
