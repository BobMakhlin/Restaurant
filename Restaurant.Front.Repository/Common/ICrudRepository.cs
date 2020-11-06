using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Front.Repository.Common
{
    public interface ICrudRepository<T, TKey> where T : class, new()
    {
        IQueryable<T> GetAll();
        Task<T> GetAsync(TKey id);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        Task AddAsync(T obj);
        void Update(T obj);
        void Delete(T obj);

        Task SaveAsync();
    }
}
