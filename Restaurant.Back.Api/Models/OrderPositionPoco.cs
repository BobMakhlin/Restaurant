using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Models
{
    public class OrderPositionPoco
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
