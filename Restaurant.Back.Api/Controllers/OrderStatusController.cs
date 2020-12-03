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
    public class OrderStatusController : ControllerBase
    {
        ICrudService<OrderStatusDto, int> orderStatusService;

        public OrderStatusController(ICrudService<OrderStatusDto, int> orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        #region GetAll by GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll()
        {
            try
            {
                var orderStatuses = await orderStatusService.GetAll().ToListAsync();
                return Ok(orderStatuses);
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
        public async Task<ActionResult<OrderStatusDto>> Get(int id)
        {
            try
            {
                var orderStatus = await orderStatusService.GetAsync(id);
                if (orderStatus == null) return NotFound();
                return Ok(orderStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion

        #region Add by POST
        [HttpPost]
        public async Task<ActionResult<OrderStatusDto>> Add([FromBody] OrderStatusDto orderStatus)
        {
            try
            {
                var insertedOrderStatus = await orderStatusService.AddAsync(orderStatus);
                return CreatedAtAction(nameof(Get), new { id = insertedOrderStatus.Id }, insertedOrderStatus);
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
        public async Task<ActionResult<OrderStatusDto>> Update(int id, [FromBody] OrderStatusDto orderStatus)
        {
            if (id != orderStatus.Id) return BadRequest();
            try
            {
                var updatedOrderStatus = await orderStatusService.UpdateAsync(orderStatus);
                return Ok(updatedOrderStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        #endregion
    }
}
