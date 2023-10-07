
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class CartRepository : GenericRepository<Cart, DataContext>, ICartRepository
    {
        private readonly DataContext context;
        public CartRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Cart>> GetVisitorCarts(string visitorId)
        {
            return await context.Cart.Where(c => c.VisitorID == visitorId).ToListAsync();
        }

        public Cart GetUserIdCart(string userId)
        {
           return context.Cart.Where(x=>x.UserId == userId).FirstOrDefault();
        }
         
        public  Cart GetVisitorCart(string visitorId)
        {
            // VisitorId'si belirtilen ziyaretçiye ait sepetleri getir
            return  context.Cart
            .Where(cart => cart.VisitorID == visitorId)
            .FirstOrDefault();
        }

        public Task<Cart> GetProductControl(int productId)
        {
            return context.Cart.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
