using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class ProductLabelDto
    {
        public int Id { get; set; }
        public int LabelId { get; set; }
        public int ProductId { get; set; }
    }
}
