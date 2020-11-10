using System;

namespace Restaurant.Back.BLL.Models
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrderId { get; set; }
        public DateTime Time { get; set; }
    }
}
