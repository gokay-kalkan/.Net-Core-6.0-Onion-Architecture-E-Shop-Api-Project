
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        List<Product> ProductListByCategory(int categoryId);
    }
}
