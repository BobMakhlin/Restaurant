using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    public partial class ProductLabel
    {
        [Key]
        public int Id { get; set; }
        public int LabelId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(LabelId))]
        [InverseProperty("ProductLabel")]
        public virtual Label Label { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductLabel")]
        public virtual Product Product { get; set; }
    }
}
