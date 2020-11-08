using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class IngredientPoco
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
    }
}
