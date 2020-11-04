namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class OrderPosition
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
