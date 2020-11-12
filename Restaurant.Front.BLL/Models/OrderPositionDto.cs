using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class OrderPositionDto
    {
        public int Id { get; set; }
        public int OrderInfoId { get; set; }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }

        public int Amount { get; set; }
    }
}
