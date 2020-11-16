using System.ComponentModel.DataAnnotations;

namespace Restaurant.Back.Blazor.Models
{
    public class OrderPositionPoco
    {
        [Key]
        public int Id { get; set; }
        
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        
        public string ProductTitle { get; set; }
        
        public decimal ProductPrice { get; set; }

        public int Amount { get; set; }
    }
}
