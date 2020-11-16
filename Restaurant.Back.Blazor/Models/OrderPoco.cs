using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Back.Blazor.Models
{
    public class OrderPoco
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime DeliveryTime { get; set; }

        [Display(Name = "Name")]
        [Required]
        [StringLength(64)]
        public string CustomerName { get; set; }

        [Display(Name = "Phone")]
        [Required]
        [StringLength(64)]
        public string CustomerPhone { get; set; }

        [Display(Name = "Address")]
        [Required]
        [StringLength(128)]
        public string CustomerAddress { get; set; }

        [StringLength(128)]
        public string Comment { get; set; }

        public List<OrderPositionPoco> OrderPosition { get; set; }
        public List<OrderStatusPoco> OrderStatus { get; set; }
    }
}
