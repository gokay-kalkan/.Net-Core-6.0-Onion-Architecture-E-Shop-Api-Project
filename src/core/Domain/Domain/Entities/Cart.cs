

using Shared.Commons;

namespace Domain.Entities
{
    public class Cart:BaseEntity
    {
        public int Quantity { get; set; }


        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        public string ProductImage { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string? VisitorID { get; set; }
        public bool IsActive { get; set; } // Ziyaretçi sepetinin aktif durumu
        public string? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
