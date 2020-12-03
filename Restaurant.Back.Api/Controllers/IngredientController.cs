using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;

namespace Restaurant.Back.Api.Controllers
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
            catch (Exception e)
            {
                return StatusCode(500, e);
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
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Add([FromBody] IngredientDto ingredient)
        {
            try
            {
                var insertedIngredient = await ingredientService.AddAsync(ingredient);
                return CreatedAtAction(nameof(Get), new { id = insertedIngredient.Id }, insertedIngredient);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(int id, [FromBody] IngredientDto ingredient)
        {
            if (id != ingredient.Id) return BadRequest();
            try
            {
                var updatedIngredient = await ingredientService.UpdateAsync(ingredient);
                return Ok(updatedIngredient);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    
    }
}
