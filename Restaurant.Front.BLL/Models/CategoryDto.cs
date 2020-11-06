using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class CategoryDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Title { get; set; }
        public bool IsEnabled { get; set; }
    }
}
