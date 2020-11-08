using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class ProductPoco
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsEnabled { get; set; }

        public CategoryPoco Category { get; set; }

        public List<IngredientPoco> Ingredients { get; set; }
    }
}
