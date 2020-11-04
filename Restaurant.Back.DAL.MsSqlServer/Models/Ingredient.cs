using System.Collections.Generic;

namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            ProductIngredient = new HashSet<ProductIngredient>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}
