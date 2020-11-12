using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Front.Api.Helpers;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Restaurant.Front.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderInfoController : ControllerBase
    {
        #region Fields
        ICrudService<OrderInfoDto, int> m_ordersService;
        ICrudService<OrderPositionDto, int> m_orderPositionService;
        #endregion

        public OrderInfoController
        (
            ICrudService<OrderInfoDto, int> ordersService,
            ICrudService<OrderPositionDto, int> orderPositionService
        )
        {
            m_ordersService = ordersService;
            m_orderPositionService = orderPositionService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderInfoDto>>> GetOrderInfos()
        {
            try
            {
                var orders = await m_ordersService
                    .GetAll()
                    .ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderInfoDto>> GetOrderInfo(int id)
        {
            try
            {
                var order = await m_ordersService.GetAsync(id);

                if (order == null)
                {
                    return NotFound();
                }


                return Ok(order);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("CurrentCart/{userId}")]
        public async Task<ActionResult<OrderInfoDto>> GetCurrentCart(Guid userId)
        {
            try
            {
                var cart = await m_ordersService
                    .Where(order => order.UserId == userId && !order.IsFinished)
                    .FirstOrDefaultAsync();

                if (cart == null)
                {
                    return NotFound();
                }


                return Ok(cart);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        public async Task<ActionResult<OrderInfoDto>> PostOrderInfo(OrderInfoDto order)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var insertedOrder = await m_ordersService.AddAsync(order);

                var orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
                await orderPositionHelper.AddOrderPositionAsync(insertedOrder.Id, order.OrderPosition);



                var target = await m_ordersService.GetAsync(insertedOrder.Id);

                transaction.Complete();


                return CreatedAtAction(nameof(GetOrderInfo), new { id = target.Id }, target);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderInfoDto>> UpdateOrderInfo(int id, OrderInfoDto order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                await m_ordersService.UpdateAsync(order);

                var orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
                await orderPositionHelper.DeleteOrderPositionAsync(order.Id);
                await orderPositionHelper.AddOrderPositionAsync(order.Id, order.OrderPosition);


                var target = await m_ordersService.GetAsync(id);

                transaction.Complete();

                return Ok(target);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            finally
            {
                transaction.Dispose();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderInfo(int id)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var target = await m_ordersService.GetAsync(id);

                if (target == null)
                {
                    return NotFound();
                }

                var orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
                await orderPositionHelper.DeleteOrderPositionAsync(target.Id);

                await m_ordersService.DeleteAsync(target);


                transaction.Complete();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
