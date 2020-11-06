using Microsoft.EntityFrameworkCore;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Back.DAL.MsSqlServer.Repositories
{
    public class OrderRepository : CrudRepository<Order, int>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }

        private IQueryable<Order> GetOrders() => m_table
            .Include(item => item.OrderStatus).ThenInclude(item => item.Status);


        public override IQueryable<Order> GetAll()
        {
            return GetOrders();
        }

        public override Task<Order> GetAsync(int id)
        {
            return GetOrders()
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override IQueryable<Order> Where(Expression<Func<Order, bool>> predicate)
        {
            return GetOrders()
                .Where(predicate);
        }
    }
}