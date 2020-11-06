using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    public partial class ProductIngredient
    {
        [Key]
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [ForeignKey(nameof(IngredientId))]
        [InverseProperty("ProductIngredient")]
        public virtual Ingredient Ingredient { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductIngredient")]
        public virtual Product Product { get; set; }
    }
}
