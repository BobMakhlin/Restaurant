using Microsoft.EntityFrameworkCore;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Helpers
{
    public class ProductIngredientHelper
    {
        #region Fields
        ICrudService<ProductIngredientDto, int> m_productIngredientService;
        #endregion

        #region Constructors
        public ProductIngredientHelper(ICrudService<ProductIngredientDto, int> productIngredientService)
        {
            m_productIngredientService = productIngredientService;
        }
        #endregion

        #region Methods
        public async Task AddIngredientsAsync(int productId, IEnumerable<IngredientDto> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                var productIngredient = new ProductIngredientDto
                {
                    IngredientId = ingredient.Id,
                    ProductId = productId
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
        #endregion
    }
}
