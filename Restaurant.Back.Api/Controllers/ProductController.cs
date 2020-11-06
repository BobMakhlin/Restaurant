using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Back.Api.Helpers;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;

namespace Restaurant.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        ICrudService<ProductDto, int> m_productsService;
        ICrudService<IngredientDto, int> m_ingredientsService;
        ICrudService<ProductIngredientDto, int> m_productIngredientService;
        #endregion

        public ProductController
        (
            ICrudService<ProductDto, int> productsService,
            ICrudService<IngredientDto, int> ingredientsService,
            ICrudService<ProductIngredientDto, int> productIngredientService
        )
        {
            m_productsService = productsService;
            m_ingredientsService = ingredientsService;
            m_productIngredientService = productIngredientService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await m_productsService.GetAll().ToListAsync();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(int id)
        {
            try
            {
                var products = await m_productsService
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var insertedProduct = await m_productsService.AddAsync(product);


                var productsHelper = new ProductIngredientHelper(m_productIngredientService);
                await productsHelper.AddIngredientsAsync(insertedProduct.Id, product.Ingredients);


                var targetProduct = await m_productsService.GetAsync(insertedProduct.Id);



                transaction.Complete();


                return CreatedAtAction
                (
                    nameof(GetProduct),
                    new { id = targetProduct.Id },
                    targetProduct
                );
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }


            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await m_productsService.UpdateAsync(product);


                var productsHelper = new ProductIngredientHelper(m_productIngredientService);
                await productsHelper.DeleteIngredientsAsync(product.Id);
                await productsHelper.AddIngredientsAsync(product.Id, product.Ingredients);

                var targetProduct = await m_productsService.GetAsync(product.Id);


                transaction.Complete();

                
                return Ok(targetProduct);
            }
            catch (Exception)
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
