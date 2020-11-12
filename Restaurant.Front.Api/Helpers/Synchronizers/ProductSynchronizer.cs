using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers.Synchronizers
{
    public class ProductSynchronizer
    {
        #region Fields
        ICrudService<ProductDto, int> m_productsService;
        ProductIngredientHelper m_productIngredientHelper;
        #endregion

        public ProductSynchronizer
        (
            ICrudService<ProductDto, int> productsService,
            ProductIngredientHelper productIngredientHelper
        )
        {
            m_productsService = productsService;
            m_productIngredientHelper = productIngredientHelper;
        }


        public async Task SynchronizeAsync(List<ProductDto> backProducts)
        {
            foreach (var backProduct in backProducts)
            {
                var frontProduct = await m_productsService.GetAsync(backProduct.Id);


                if (frontProduct == null)
                {
                    await AddProductAsync(backProduct);
                }
                else
                {
                    await UpdateProductAsync(frontProduct, backProduct);
                }
            }
        }

        private async Task AddProductAsync(ProductDto backProduct)
        {
            await m_productsService.AddAsync(backProduct);

            await m_productIngredientHelper.AddIngredientsAsync(backProduct.Id, backProduct.Ingredients);
        }
        private async Task UpdateProductAsync(ProductDto frontProduct, ProductDto backProduct)
        {
            frontProduct.Weight = backProduct.Weight;
            frontProduct.Price = backProduct.Price;
            frontProduct.IsEnabled = backProduct.IsEnabled;

            await m_productsService.UpdateAsync(frontProduct);

            await m_productIngredientHelper.UpdateIngredientsAsync(backProduct.Id, backProduct.Ingredients);
        }
    }
}
