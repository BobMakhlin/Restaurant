using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    [Table("OrderPosition")]
    public partial class OrderPosition
    {
        [Key]
        public int Id { get; set; }
        public int OrderInfoId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        [ForeignKey(nameof(OrderInfoId))]
        [InverseProperty("OrderPosition")]
        public virtual OrderInfo OrderInfo { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderPosition")]
        public virtual Product Product { get; set; }
    }
}
