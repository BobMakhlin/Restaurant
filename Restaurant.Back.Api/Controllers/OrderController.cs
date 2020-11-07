using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Back.Api.Helpers;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;

namespace Restaurant.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Fields
        ICrudService<OrderDto, int> m_ordersService;
        ICrudService<OrderStatusDto, int> m_orderStatusService;
        ICrudService<StatusDto, int> m_statusesService;
        #endregion

        public OrderController
        (
            ICrudService<OrderDto, int> ordersService,
            ICrudService<OrderStatusDto, int> orderStatusService,
            ICrudService<StatusDto, int> statusesService
        )
        {
            m_ordersService = ordersService;
            m_orderStatusService = orderStatusService;
            m_statusesService = statusesService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            try
            {
                var orders = await m_ordersService.GetAll().ToListAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrder(int id)
        {
            try
            {
                var order = await m_ordersService.GetAsync(id);
                return Ok(order);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost] 
        public async Task<ActionResult<OrderDto>> AddOrder(OrderDto order)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var insertedOrder = await m_ordersService.AddAsync(order);

                var helper = new OrderStatusHelper(m_orderStatusService);
                await helper.AddStatusesAsync(insertedOrder.Id, order.Statuses);

                transaction.Complete();

                return CreatedAtAction(nameof(GetOrder), new { id = insertedOrder.Id }, insertedOrder);
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

        [HttpPut]
        public async Task<ActionResult<OrderDto>> UpdateOrder(int id, OrderDto order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await m_ordersService.UpdateAsync(order);

                var helper = new OrderStatusHelper(m_orderStatusService);
                await helper.DeleteStatusesAsync(order.Id);
                await helper.AddStatusesAsync(order.Id, order.Statuses);

                transaction.Complete();

                return Ok(order);
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
    }
}
