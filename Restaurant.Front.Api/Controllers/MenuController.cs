using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Front.Api.Helpers;
using Restaurant.Front.Api.Helpers.Synchronizers;
using Restaurant.Front.Api.Models;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        #region Fields
        ICrudService<ProductDto, int> m_productsService;
        ICrudService<IngredientDto, int> m_ingredientsService;
        ICrudService<ProductIngredientDto, int> m_productIngredientService;
        ICrudService<CategoryDto, int> m_categoriesService;

        ProductIngredientHelper m_productIngredientHelper;
        #endregion

        public MenuController
        (
            ICrudService<ProductDto, int> productsServices,
            ICrudService<IngredientDto, int> ingredientsServices,
            ICrudService<ProductIngredientDto, int> productIngredientService,
            ICrudService<CategoryDto, int> categoriesServices
        )
        {
            m_productsService = productsServices;
            m_ingredientsService = ingredientsServices;
            m_productIngredientService = productIngredientService;
            m_categoriesService = categoriesServices;

            m_productIngredientHelper = new ProductIngredientHelper(m_productIngredientService);
        }



        [HttpPost]
        public async Task<ActionResult> PostMenuAsync(MenuPoco menu)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var categorySync = new CategorySynchronizer
                (
                    m_categoriesService
                );
                await categorySync.SynchronizeAsync(menu.Categories);

                var ingredientSync = new IngredientSynchronizer
                (
                    m_ingredientsService
                );
                await ingredientSync.SynchronizeAsync(menu.Ingredients);


                var productSync = new ProductSynchronizer
                (
                    m_productsService,
                    m_productIngredientHelper
                );
                await productSync.SynchronizeAsync(menu.Products);



                transaction.Complete();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
