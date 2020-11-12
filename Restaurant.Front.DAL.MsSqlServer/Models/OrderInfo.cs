using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Restaurant.Front.DAL.MsSqlServer.Models
{
    [Table("OrderInfo")]
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            OrderPosition = new HashSet<OrderPosition>();
        }

        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeliveryTime { get; set; }
        [StringLength(64)]
        public string CustomerName { get; set; }
        [StringLength(128)]
        public string CustomerAddress { get; set; }
        [StringLength(14)]
        public string CustomerPhone { get; set; }
        [StringLength(128)]
        public string Comment { get; set; }
        public bool IsFinished { get; set; }

        [InverseProperty("OrderInfo")]
        public virtual ICollection<OrderPosition> OrderPosition { get; set; }
    }
}
