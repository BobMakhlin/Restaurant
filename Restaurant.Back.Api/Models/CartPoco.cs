using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Models
{
    public class CartPoco
    {
        public List<OrderPositionPoco> OrderPosition { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string Comment { get; set; }
    }
}
