using System.Collections.Generic;

namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderPosition = new HashSet<OrderPosition>();
            ProductIngredient = new HashSet<ProductIngredient>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsEnabled { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderPosition> OrderPosition { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}
