using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class ProductDto
    {
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


        public CategoryDto Category { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
        public List<LabelDto> Labels { get; set; }
    }
}
