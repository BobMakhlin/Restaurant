using System;
using System.Collections.Generic;

namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderPosition = new HashSet<OrderPosition>();
            OrderStatus = new HashSet<OrderStatus>();
        }

        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<OrderPosition> OrderPosition { get; set; }
        public virtual ICollection<OrderStatus> OrderStatus { get; set; }
    }
}
