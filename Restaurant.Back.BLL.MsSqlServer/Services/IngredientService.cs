using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Back.BLL.MsSqlServer.Services
{
    public class IngredientService : CrudService<Ingredient, IngredientDto, int>
    {
        public IngredientService(ICrudRepository<Ingredient, int> repository) : base(repository)
        {
        }
    }
}
