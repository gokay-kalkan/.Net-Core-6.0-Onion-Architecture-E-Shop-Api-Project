

using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        List<T> List();
        void Add(T p);

         Task<T>  AddAsync(T p);

         Task<T> UpdateAsync(T entity);
        T GetById(int id);
        T GetByIdString(string id);
        void Update(T p);
        void Delete(T p);
        void FullDelete(T p);
        List<T> List(Expression<Func<T, bool>> filter);


    }
}
