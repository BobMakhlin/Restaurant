using Microsoft.EntityFrameworkCore;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Back.DAL.MsSqlServer.Repositories
{
    public class IngredientRepository : CrudRepository<Ingredient, int>
    {
        public IngredientRepository(DbContext context) : base(context)
        {
        }
    }
}
