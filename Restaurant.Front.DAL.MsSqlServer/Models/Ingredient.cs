using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            ProductIngredient = new HashSet<ProductIngredient>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Title { get; set; }
        [Required]
        [StringLength(32)]
        public string Photo { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }

        [InverseProperty("Ingredient")]
        public virtual ICollection<ProductIngredient> ProductIngredient { get; set; }
    }
}
