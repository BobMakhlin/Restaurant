using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurant.Front.BLL.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }


        public CategoryDto Category { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
        public List<LabelDto> Labels { get; set; }
    }
}
