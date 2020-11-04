using System;
using System.Collections.Generic;

namespace Restaurant.Back.BLL.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string Comment { get; set; }

        public List<OrderPositionDto> OrderPosition { get; set; }

        // Don't show in web api.
        public List<OrderStatusDto> OrderStatus { get; set; }

        // Load in web api.
        public List<StatusDto> Statuses { get; set; }
    }
}
