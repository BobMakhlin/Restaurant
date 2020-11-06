using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class ProductIngredientDto
    {
        [Key]
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
