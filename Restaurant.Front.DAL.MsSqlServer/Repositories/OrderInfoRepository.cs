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
    public class OrderInfoRepository : CrudRepository<OrderInfo, int>
    {
        public OrderInfoRepository(DbContext context) : base(context)
        {
        }

        private IQueryable<OrderInfo> GetOrders() => m_table
            .Include(item => item.OrderPosition)
                .ThenInclude(item => item.Product);


        public override IQueryable<OrderInfo> GetAll()
        {
            return GetOrders();
        }

        public override Task<OrderInfo> GetAsync(int id)
        {
            return GetOrders()
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public override IQueryable<OrderInfo> Where(Expression<Func<OrderInfo, bool>> predicate)
        {
            return GetOrders()
                .Where(predicate);
        }
    }
}
