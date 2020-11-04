using Microsoft.EntityFrameworkCore;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Back.DAL.MsSqlServer.Repositories
{
    public class OrderRepository : CrudRepository<Order, int>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}
