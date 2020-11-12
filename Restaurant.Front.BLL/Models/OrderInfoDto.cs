using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string Comment { get; set; }
        public bool IsFinished { get; set; }

        public virtual ICollection<OrderPositionDto> OrderPosition { get; set; }
    }
}
