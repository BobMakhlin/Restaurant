using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Back.Api.Helpers;
using Restaurant.Back.Api.Models;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using Restaurant.Back.DAL.MsSqlServer.Models;

namespace Restaurant.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region Fields
        ICrudService<OrderDto, int> m_ordersService;
        ICrudService<OrderPositionDto, int> m_orderPositionService;
        ICrudService<OrderStatusDto, int> m_orderStatusService;

        OrderPositionHelper m_orderPositionHelper;
        OrderStatusHelper m_orderStatusHelper;
        #endregion

        public CartController
        (
            ICrudService<OrderDto, int> orderService,
            ICrudService<OrderPositionDto, int> orderPositionService,
            ICrudService<OrderStatusDto, int> orderStatusService
        )
        {
            m_ordersService = orderService;
            m_orderPositionService = orderPositionService;
            m_orderStatusService = orderStatusService;

            m_orderPositionHelper = new OrderPositionHelper(m_orderPositionService);
            m_orderStatusHelper = new OrderStatusHelper(m_orderStatusService);
        }

        [HttpPost]
        public async Task<ActionResult> PostCart([FromBody] OrderDto order)
        {
            var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                order.OrderTime = DateTime.Now;
                order.OrderStatus = new List<OrderStatusDto>
                {
                    new OrderStatusDto { StatusId = 2, Time = DateTime.Now }
                };


                var insertedOrder = await m_ordersService.AddAsync(order);

                await m_orderPositionHelper.AddOrderPositionAsync(insertedOrder.Id, order.OrderPosition);
                await m_orderStatusHelper.AddOrderStatusAsync(insertedOrder.Id, order.OrderStatus);


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
