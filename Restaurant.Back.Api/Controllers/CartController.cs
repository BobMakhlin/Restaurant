using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        ICrudService<OrderDto, int> orderService;
        ICrudService<OrderPositionDto, int> orderPositionService;
        ICrudService<OrderStatusDto, int> orderStatusService;


        public CartController
            (
                ICrudService<OrderDto, int> orderService,
                ICrudService<OrderPositionDto, int> orderPositionService,
                ICrudService<OrderStatusDto, int> orderStatusService
            )
        {
            this.orderService = orderService;
            this.orderPositionService = orderPositionService;
            this.orderStatusService = orderStatusService;

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CartPoco cart)
        {
            try
            {
                var cartHelper = new CartHelper(orderService, orderPositionService, orderStatusService);
                await cartHelper.CreateCart(cart);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


    }
}
