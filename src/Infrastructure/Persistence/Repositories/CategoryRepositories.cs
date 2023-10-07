

using Application.Interfaces;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class CategoryRepositories:GenericRepository<Category,DataContext>,ICategoryRepository
    {
        private DataContext context;
        public CategoryRepositories(DataContext context) : base(context)
        {
            this.context = context;
        }

    }
}
