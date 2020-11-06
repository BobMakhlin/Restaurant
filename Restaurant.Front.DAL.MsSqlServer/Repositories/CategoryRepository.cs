using Microsoft.EntityFrameworkCore;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.DAL.MsSqlServer.Repositories
{
    public class CategoryRepository : CrudRepository<Category, int>
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
