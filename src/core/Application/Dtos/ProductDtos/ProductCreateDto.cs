
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Stock { get; set; }
       
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
       
    }
}
