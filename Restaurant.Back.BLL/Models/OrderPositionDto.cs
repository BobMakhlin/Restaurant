﻿namespace Restaurant.Back.BLL.Models
{
    public class OrderPositionDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }

        public int Amount { get; set; }

    }
}
