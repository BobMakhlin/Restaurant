using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class MenuPoco
    {
        public List<ProductPoco> Products { get; set; }
        public List<CategoryPoco> Categories { get; set; }
        public List<IngredientPoco> Ingredients { get; set; }
    }
}
