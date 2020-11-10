using Restaurant.Front.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Models
{
    public class MenuPoco
    {
        public List<ProductDto> Products { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
