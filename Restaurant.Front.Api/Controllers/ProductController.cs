using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
        ICrudService<ProductDto, int> m_productService;
        ICrudService<ProductIngredientDto, int> m_productIngredientService;
        ICrudService<ProductLabelDto, int> m_productLabelService;

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
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await m_productService.GetAll().ToListAsync();
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


        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }


            //var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await m_productService.UpdateAsync(product);


                var productIngredientHelper = new ProductIngredientHelper(m_productIngredientService);

                await productIngredientHelper.DeleteIngredientsAsync(product.Id);
                await productIngredientHelper.AddIngredientsAsync(product.Id, product.Ingredients);


                var productLabelHelper = new ProductLabelHelper(m_productLabelService);

                await productLabelHelper.DeleteLabelsAsync(product.Id);
                await productLabelHelper.AddLabelsAsync(product.Id, product.Labels);



                var insertedProduct = await m_productService.GetAsync(product.Id);



                //transaction.Complete();

                return Ok(insertedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
