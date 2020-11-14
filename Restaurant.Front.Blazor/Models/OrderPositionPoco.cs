namespace Restaurant.Front.Blazor.Models
{
    public class OrderPositionPoco
    {
        public int Id { get; set; }
        public int OrderInfoId { get; set; }

        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public decimal ProductPrice { get; set; }

        public int Amount { get; set; }
    }
}