using Microsoft.EntityFrameworkCore;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Front.DAL.MsSqlServer.Repositories
{
    public class ProductRepository : CrudRepository<Product, int>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }


        private IQueryable<Product> GetProducts() => m_table
            .Include(item => item.ProductIngredient)
                .ThenInclude(item => item.Ingredient)
            .Include(item => item.ProductLabel)
                .ThenInclude(item => item.Label);


        public override IQueryable<Product> GetAll()
        {
            return GetProducts();
        }

        public override Task<Product> GetAsync(int id)
        {
            return GetProducts()
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override IQueryable<Product> Where(Expression<Func<Product, bool>> predicate)
        {
            return GetProducts()
                .Where(predicate);
        }
    }
}
