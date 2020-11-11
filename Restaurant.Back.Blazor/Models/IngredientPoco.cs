using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class IngredientPoco
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(32)]
        public string Title { get; set; }
        
        public string Photo { get; set; }
     
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public bool IsEnabled { get; set; }
    }
}
