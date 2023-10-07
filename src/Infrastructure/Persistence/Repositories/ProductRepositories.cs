

using Application.Interfaces;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class ProductRepositories : GenericRepository<Product, DataContext>, IProductRepository
    {
        private DataContext context;
        public ProductRepositories(DataContext context) : base(context)
        {
            this.context = context;
        }

        public List<Product> ProductListByCategory(int categoryId)
        {
            return context.Products.Where(x=>x.CategoryId== categoryId).ToList();
        }
    }
}
