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
    public class LabelController : ControllerBase
    {
        ICrudService<LabelDto, int> labelService;
        public LabelController(ICrudService<LabelDto, int> labelService)
        {
            this.labelService = labelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LabelDto>>> GetAll()
        {
            try
            {
                var query = labelService.GetAll();
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
        public async Task<ActionResult<LabelDto>> Get(int id)
        {
            try
            {
                var ingredient = await labelService.GetAsync(id);
                if (ingredient == null) return NotFound();
                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<LabelDto>> Add([FromBody] LabelDto label)
        {
            try
            {
                await labelService.AddAsync(label);
                return CreatedAtAction(nameof(Get), new { id = label.Id }, label);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<LabelDto>> Update(int id, [FromBody] LabelDto label)
        {
            if (id != label.Id) return BadRequest();
            try
            {
                await labelService.UpdateAsync(label);
                return Ok(label);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LabelDto>> Delete(int id)
        {
            try
            {
                var target = await labelService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }
                await labelService.DeleteAsync(target);

                return Ok(target);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

    }
}
