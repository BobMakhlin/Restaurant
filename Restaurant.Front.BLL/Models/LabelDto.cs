using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class LabelDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(24)]
        public string Title { get; set; }
        [Required]
        [StringLength(32)]
        public string Photo { get; set; }
    }
}
