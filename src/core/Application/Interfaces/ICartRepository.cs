

using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICartRepository: IGenericRepository<Cart>
    {
        Cart GetVisitorCart(string visitorId);
        Cart GetUserIdCart(string userId);
        Task <List<Cart>> GetVisitorCarts(string visitorId);
        Task <Cart> GetProductControl(int productId);
    }
}
