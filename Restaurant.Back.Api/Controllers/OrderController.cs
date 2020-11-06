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
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var query =
                from order in m_ordersService.GetAll()

                select new OrderDto
                {
                    Statuses = (
                        from status in m_statusesService.GetAll()
                        where order.OrderStatus.Any(item => item.StatusId == status.Id)
                        select status
                    ).ToList()
                };


            return Ok(query);
        }
    }
}
