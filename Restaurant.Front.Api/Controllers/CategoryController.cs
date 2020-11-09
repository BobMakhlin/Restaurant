using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICrudService<CategoryDto, int> categoryService;
        public CategoryController(ICrudService<CategoryDto, int> categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            try
            {
                var query = categoryService.GetAll();
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
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            try
            {
                var category = await categoryService.GetAsync(id);
                if (category == null) return NotFound();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add([FromBody] CategoryDto category)
        {
            try
            {
                await categoryService.AddAsync(category);
                return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] CategoryDto category)
        {
            if (id != category.Id) return BadRequest();
            try
            {
                await categoryService.UpdateAsync(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            try
            {
                var target = await categoryService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }
                await categoryService.DeleteAsync(target);

                return Ok(target);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
