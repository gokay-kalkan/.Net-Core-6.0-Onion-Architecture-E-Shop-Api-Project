

namespace Application.Dtos.CartDtos
{
    public class CartListDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }


        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string ProductImage { get; set; }
        public int ProductId { get; set; }


        public string UserId { get; set; }
    }
}
