namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class OrderStatus
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Status Status { get; set; }
    }
}
