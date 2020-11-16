using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using LinqKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Front.Api.Helpers;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        ICrudService<ProductDto, int> m_productService;
        ICrudService<ProductIngredientDto, int> m_productIngredientService;
        ICrudService<ProductLabelDto, int> m_productLabelService;
        ProductIngredientHelper m_productIngredientHelper;
        ProductLabelHelper m_productLabelHelper;
        #endregion


        public ProductController
        (
            ICrudService<ProductDto, int> productService,
            ICrudService<ProductIngredientDto, int> productIngredientService,
            ICrudService<ProductLabelDto, int> productLabelService
        )
        {
            m_productService = productService;
            m_productIngredientService = productIngredientService;
            m_productLabelService = productLabelService;

            m_productIngredientHelper = new ProductIngredientHelper(m_productIngredientService);
            m_productLabelHelper = new ProductLabelHelper(m_productLabelService);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var predicateBuilder = PredicateBuilder.New<ProductDto>();

                var predicate = predicateBuilder
                    .And(product => product.IsEnabled)
                    .And(product => product.Category.IsEnabled);


                var products = await m_productService
                    .Where(predicate)
                    .ToListAsync();


                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct(int id)
        {
            try
            {
                var product = await m_productService
                    .Where(item => item.Id == id)
                    .FirstOrDefaultAsync();

                return Ok(product);
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
                var addedProduct = await m_productService.AddAsync(product);
                var productId = addedProduct.Id;


                await m_productIngredientHelper.AddIngredientsAsync(productId, product.Ingredients);
                await m_productLabelHelper.AddLabelsAsync(productId, product.Labels);


                var target = await m_productService.GetAsync(productId);


                transaction.Complete();

                return CreatedAtAction
                (
                    nameof(GetProduct),
                    new { Id = productId },
                    target
                );
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
                await m_productService.UpdateAsync(product);


                await m_productIngredientHelper.DeleteIngredientsAsync(product.Id);
                await m_productIngredientHelper.AddIngredientsAsync(product.Id, product.Ingredients);


                await m_productLabelHelper.DeleteLabelsAsync(product.Id);
                await m_productLabelHelper.AddLabelsAsync(product.Id, product.Labels);



                var insertedProduct = await m_productService.GetAsync(product.Id);



                transaction.Complete();

                return Ok(insertedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var target = await m_productService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }

                await m_productIngredientHelper.DeleteIngredientsAsync(target.Id);
                await m_productLabelHelper.DeleteLabelsAsync(target.Id);
                await m_productService.DeleteAsync(target);


                transaction.Complete();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
