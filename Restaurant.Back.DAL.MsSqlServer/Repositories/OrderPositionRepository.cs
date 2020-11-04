using Microsoft.EntityFrameworkCore;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Back.DAL.MsSqlServer.Repositories
{
    public class OrderPositionRepository : CrudRepository<OrderPosition, int>
    {
        public OrderPositionRepository(DbContext context) : base(context)
        {
        }
    }
}
