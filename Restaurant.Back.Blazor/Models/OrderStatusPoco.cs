using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class OrderStatusPoco
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string StatusTitle { get; set; }
        public int OrderId { get; set; }
        public DateTime Time { get; set; }
    }
}
