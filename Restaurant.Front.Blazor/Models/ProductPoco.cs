﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Blazor.Models
{
    public class ProductPoco
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
        public List<LabelPoco> Labels { get; set; }
        public List<IngredientPoco> Ingredients { get; set; }
    }
}
