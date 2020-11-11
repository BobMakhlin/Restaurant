using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class ProductPoco
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

        public bool IsEnabled { get; set; }


        public CategoryPoco Category { get; set; }

        public List<IngredientPoco> Ingredients { get; set; }
    }
}
