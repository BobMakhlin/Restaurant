using System.Collections.Generic;

namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
