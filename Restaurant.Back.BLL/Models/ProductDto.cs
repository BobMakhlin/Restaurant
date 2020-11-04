﻿using System.Collections.Generic;

namespace Restaurant.Back.BLL.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Weight { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public bool IsEnabled { get; set; }

        public CategoryDto CategoryDto { get; set; }

        // Don't show in web api.
        public List<ProductIngredientDto> ProductIngredients { get; set; }

        // Load in web api.
        public List<IngredientDto> Ingredients { get; set; }
    }
}
