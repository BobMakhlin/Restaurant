using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class IngredientDto
    {
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
    }
}
