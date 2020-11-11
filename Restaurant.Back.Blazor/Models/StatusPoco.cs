using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class StatusPoco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime Time { get; set; }
    }
}
