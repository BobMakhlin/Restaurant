using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class ProductIngredientService : CrudService<ProductIngredient, ProductIngredientDto, int>
    {
        public ProductIngredientService(ICrudRepository<ProductIngredient, int> repository) : base(repository)
        {
        }
    }
}
