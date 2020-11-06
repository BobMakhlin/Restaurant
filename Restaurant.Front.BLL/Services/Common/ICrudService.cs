using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Front.BLL.Services.Common
{
    public interface ICrudService<T, TKey>
       where T : class
    {
        // Read.
        IQueryable<T> GetAll();
        Task<T> GetAsync(TKey key);

        // Create.
        Task<T> AddAsync(T item);

        // Update.
        Task<T> UpdateAsync(T item);

        // Delete.
        Task<T> DeleteAsync(T item);

        // Filter.
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
