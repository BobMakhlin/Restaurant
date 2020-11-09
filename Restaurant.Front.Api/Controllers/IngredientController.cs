using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        ICrudService<IngredientDto, int> ingredientService;
        public IngredientController(ICrudService<IngredientDto, int> ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAll()
        {
            try
            {
                var query = ingredientService.GetAll();
                var result = await query.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IngredientDto>> Get(int id)
        {
            try
            {
                var ingredient = await ingredientService.GetAsync(id);
                if (ingredient == null) return NotFound();
                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Add([FromBody] IngredientDto ingredient)
        {
            try
            {
                await ingredientService.AddAsync(ingredient);
                return CreatedAtAction(nameof(Get), new { id = ingredient.Id }, ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(int id, [FromBody] IngredientDto ingredient)
        {
            if (id != ingredient.Id) return BadRequest();
            try
            {
                await ingredientService.UpdateAsync(ingredient);
                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IngredientDto>> Delete(int id)
        {
            try
            {
                var target = await ingredientService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }
                await ingredientService.DeleteAsync(target);

                return Ok(target);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
