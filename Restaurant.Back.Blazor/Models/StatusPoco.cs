using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class StatusPoco
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}
