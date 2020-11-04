using System;
using System.Collections.Generic;

namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class Status
    {
        public Status()
        {
            OrderStatus = new HashSet<OrderStatus>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        public virtual ICollection<OrderStatus> OrderStatus { get; set; }
    }
}
