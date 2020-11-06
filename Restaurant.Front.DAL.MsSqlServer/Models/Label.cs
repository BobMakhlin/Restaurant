using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    public partial class Label
    {
        public Label()
        {
            ProductLabel = new HashSet<ProductLabel>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(24)]
        public string Title { get; set; }
        [Required]
        [StringLength(32)]
        public string Photo { get; set; }

        [InverseProperty("Label")]
        public virtual ICollection<ProductLabel> ProductLabel { get; set; }
    }
}
