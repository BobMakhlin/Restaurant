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
    public class OrderPositionController : ControllerBase
    {
        ICrudService<OrderPositionDto, int> orderPositionService;

        public OrderPositionController(ICrudService<OrderPositionDto, int> orderPositionService)
        {
            this.orderPositionService = orderPositionService;
        }

        #region GetAll by GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderPositionDto>>> GetAll()
        {
            try
            {
                var orderPositions = await orderPositionService.GetAll().ToListAsync();
                return Ok(orderPositions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Get by GET
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderPositionDto>> Get(int id)
        {
            try
            {
                var orderPosition = await orderPositionService.GetAsync(id);
                if (orderPosition == null) return NotFound();
                return Ok(orderPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Add by POST
        [HttpPost]
        public async Task<ActionResult<OrderPositionDto>> Add([FromBody] OrderPositionDto orderPosition)
        {
            try
            {
                await orderPositionService.AddAsync(orderPosition);
                return CreatedAtAction(nameof(Get), new { id = orderPosition.Id }, orderPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Update by PUT
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<OrderPositionDto>> Update(int id, [FromBody] OrderPositionDto orderPosition)
        {
            if (id != orderPosition.Id) return BadRequest();
            try
            {
                await orderPositionService.UpdateAsync(orderPosition);
                return Ok(orderPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion
    }
}
