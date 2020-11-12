using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductIngredient = new HashSet<ProductIngredient>();
            ProductLabel = new HashSet<ProductLabel>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Title { get; set; }
        [StringLength(128)]
        public string Description { get; set; }
        [StringLength(32)]
        public string Photo { get; set; }
        public double Weight { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsEnabled { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Product")]
        public virtual Category Category { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<OrderPosition> OrderPosition { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductLabel> ProductLabel { get; set; }
    }
}
