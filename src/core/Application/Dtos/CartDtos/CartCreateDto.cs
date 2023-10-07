

using Domain.Entities;

namespace Application.Dtos.CartDtos
{
   public class CartCreateDto
    {

        public int Quantity { get; set; }


        public DateTime Date { get; set; }

     
        public int ProductId { get; set; }

     
        public string? VisitorId { get; set; }

    }
}
