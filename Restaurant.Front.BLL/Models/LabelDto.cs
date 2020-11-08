using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class LabelDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
    }
}
