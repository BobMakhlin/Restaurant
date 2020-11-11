using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class CategoryPoco
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(24)]
        public string Title { get; set; }
        public bool IsEnabled { get; set; }
    }
}
