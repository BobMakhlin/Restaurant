using Microsoft.EntityFrameworkCore;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers
{
    public class ProductIngredientHelper
    {
        ICrudService<ProductIngredientDto, int> m_productIngredientService;

        public ProductIngredientHelper(ICrudService<ProductIngredientDto, int> productIngredientService)
        {
            m_productIngredientService = productIngredientService;
        }


        public async Task AddIngredientsAsync(int productId, List<IngredientDto> ingredients)
        {
            foreach (var item in ingredients)
            {
                var productIngredient = new ProductIngredientDto()
                {
                    ProductId = productId,
                    IngredientId = item.Id
                };

                await m_productIngredientService.AddAsync(productIngredient);
            }
        }

        public async Task DeleteIngredientsAsync(int productId)
        {
            var productIngredients = await m_productIngredientService
                .Where(item => item.ProductId == productId)
                .ToListAsync();

            foreach (var item in productIngredients)
            {
                await m_productIngredientService.DeleteAsync(item);
            }
        }
    }
}
