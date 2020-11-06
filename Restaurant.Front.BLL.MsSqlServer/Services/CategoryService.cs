using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class CategoryService : CrudService<Category, CategoryDto, int>
    {
        public CategoryService(ICrudRepository<Category, int> repository) : base(repository)
        {
        }
    }
}
