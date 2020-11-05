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
    public class StatusController : ControllerBase
    {
        ICrudService<StatusDto, int> statusService;
        public StatusController(ICrudService<StatusDto, int> statusService)
        {
            this.statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            try
            {
                var query = statusService.GetAll();
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
        public async Task<ActionResult<StatusDto>> Get(int id)
        {
            try
            {
                var status = await statusService.GetAsync(id);
                if (status == null) return NotFound();
                return Ok(status);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StatusDto>> Add([FromBody] StatusDto status)
        {
            try
            {
                await statusService.AddAsync(status);
                return CreatedAtAction(nameof(Get), new { id = status.Id }, status);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<StatusDto>> Update(int id, [FromBody] StatusDto status)
        {
            if (id != status.Id) return BadRequest();
            try
            {
                await statusService.UpdateAsync(status);
                return Ok(status);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}
