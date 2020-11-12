using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPositionController : ControllerBase
    {
        #region Fields
        ICrudService<OrderPositionDto, int> m_orderPositionService;
        #endregion

        public OrderPositionController(ICrudService<OrderPositionDto, int> orderPositionService)
        {
            m_orderPositionService = orderPositionService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderPositionDto>>> GetOrderPositions()
        {
            try
            {
                var orderPositions = await m_orderPositionService
                    .GetAll()
                    .ToListAsync();

                return Ok(orderPositions);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderPositionDto>> GetOrderPosition(int id)
        {
            try
            {
                var orderPosition = await m_orderPositionService.GetAsync(id);

                if (orderPosition == null)
                {
                    return NotFound();
                }

                return Ok(orderPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult<OrderPositionDto>> PostOrderPosition([FromBody] OrderPositionDto orderPosition)
        {
            try
            {
                var insertedOrderPosition = await m_orderPositionService.AddAsync(orderPosition);

                return CreatedAtAction
                (
                    nameof(GetOrderPosition), 
                    new { id = insertedOrderPosition.Id },
                    insertedOrderPosition
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<OrderPositionDto>> PutOrderPosition(int id, [FromBody] OrderPositionDto orderPosition)
        {
            if (id != orderPosition.Id)
            {
                return BadRequest();
            }

            try
            {
                var updatedPosition = await m_orderPositionService.UpdateAsync(orderPosition);

                return Ok(updatedPosition);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<LabelDto>> Delete(int id)
        {
            try
            {
                var target = await m_orderPositionService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }

                var deletedItem = await m_orderPositionService.DeleteAsync(target);

                return Ok(deletedItem);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }
    }
}
