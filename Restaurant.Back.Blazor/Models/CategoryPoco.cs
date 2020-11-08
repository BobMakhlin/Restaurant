using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Blazor.Models
{
    public class CategoryPoco
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsEnabled { get; set; }
    }
}
