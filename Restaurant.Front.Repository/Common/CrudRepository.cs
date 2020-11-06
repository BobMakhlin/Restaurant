using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurant.Front.Repository.Common
{
    public abstract class CrudRepository<T, TKey> : ICrudRepository<T, TKey>
        where T : class, new()
    {
        #region Fields
        protected DbContext m_context;
        protected DbSet<T> m_table;
        #endregion

        public CrudRepository(DbContext context)
        {
            m_context = context;
            m_table = context.Set<T>();
        }

        #region Methods
        public virtual IQueryable<T> GetAll()
        {
            return m_table;
        }
        public virtual async Task<T> GetAsync(TKey id)
        {
            return await m_table.FindAsync(id);
        }


        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return m_table.Where(predicate);
        }


        public async Task AddAsync(T obj)
        {
            await m_table.AddAsync(obj);
        }
        public void Update(T obj)
        {
            m_table.Update(obj);
        }
        public void Delete(T item)
        {
            m_table.Remove(item);
            //m_table.Remove(item);
        }


        public async Task SaveAsync()
        {
            await m_context.SaveChangesAsync();
        }
        #endregion
    }
}
