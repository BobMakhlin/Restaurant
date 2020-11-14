using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Blazor.Models
{
    public class OrderPoco
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "delivery time")]
        public DateTime? DeliveryTime { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "name")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "address")]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(14)]
        [Display(Name = "phone")]
        public string CustomerPhone { get; set; }

        [StringLength(128)]
        public string Comment { get; set; }

        public bool IsFinished { get; set; }

        public virtual List<OrderPositionPoco> OrderPosition { get; set; }
    }
}
