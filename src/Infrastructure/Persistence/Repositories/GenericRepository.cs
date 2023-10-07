

using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class GenericRepository<T,TContext> : IGenericRepository<T> where T : class, new() where TContext : DbContext, new()
    {
        private DataContext context = new DataContext();
        DbSet<T> _object;
        public GenericRepository(DataContext context)
        {
            this.context = context;
            _object = context.Set<T>();
        }
        public void Add(T p)
        {
            _object.Add(p);
            context.SaveChanges();
        }

        public async Task<T> AddAsync(T entity)
        {
            _object.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public void Delete(T p)
        {
            _object.Update(p);
            context.SaveChanges();
        }

        public void FullDelete(T p)
        {
            _object.Remove(p);
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _object.Find(id);
        }

        public T GetByIdString(string id)
        {
            return _object.Find(id);
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            _object.Update(p);
            context.SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _object.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
