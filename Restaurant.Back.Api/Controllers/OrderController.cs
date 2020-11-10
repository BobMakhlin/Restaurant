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
        ICrudService<OrderPositionDto, int> m_orderPositionService;
        #endregion

        public OrderController
        (
            ICrudService<OrderDto, int> ordersService,
            ICrudService<OrderStatusDto, int> orderStatusService,
            ICrudService<StatusDto, int> statusesService,
            ICrudService<OrderPositionDto, int> orderPositionService
        )
        {
            m_ordersService = ordersService;
            m_orderStatusService = orderStatusService;
            m_statusesService = statusesService;
            m_orderPositionService = orderPositionService;
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

                var statusHelper = new OrderStatusHelper(m_orderStatusService);
                await statusHelper.AddStatusesAsync(insertedOrder.Id, order.Statuses);

                //Не используется т.к. работает Automapper
                var orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
                await orderPositionHelper.AddOrderPositionAsync(insertedOrder.Id, order.OrderPosition);

                var target = await m_ordersService.GetAsync(insertedOrder.Id);


                transaction.Complete();

                return CreatedAtAction(nameof(GetOrder), new { id = target.Id }, target);
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

                //Не используется т.к. работает Automapper

                var orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
                await orderPositionHelper.DeleteOrderPositionAsync(order.Id);
                await orderPositionHelper.AddOrderPositionAsync(order.Id, order.OrderPosition);

                var statusHelper = new OrderStatusHelper(m_orderStatusService);
                await statusHelper.DeleteStatusesAsync(order.Id);
                await statusHelper.AddStatusesAsync(order.Id, order.Statuses);

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
    }
}

//{ "Не удалось вставить значение NULL в столбец \"Title\", таблицы \"RestaurantBack.dbo.Product\"; в столбце запрещены значения NULL. Ошибка в INSERT.\r\nВыполнение данной инструкции было прервано."}