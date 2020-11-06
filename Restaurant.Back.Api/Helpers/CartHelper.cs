using Restaurant.Back.Api.Models;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Helpers
{
    public class CartHelper
    {
        ICrudService<OrderDto, int> orderService;
        ICrudService<OrderPositionDto, int> orderPositionService;
        ICrudService<OrderStatusDto, int> orderStatusService;

        public CartHelper
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

        public async Task CreateCart(CartPoco cart)
        {
            OrderDto orderDto = await CreateOrder(cart);
            await CreateOrderPosition(cart, orderDto);
            await CreateOrderStatus(orderDto);
        }

        private async Task CreateOrderStatus(OrderDto orderDto)
        {
            OrderStatusDto orderStatus = new OrderStatusDto
            {
                OrderId = orderDto.Id,
                StatusId = 1
            };
            await orderStatusService.AddAsync(orderStatus);
        }

        private async Task CreateOrderPosition(CartPoco cart, OrderDto orderDto)
        {
            foreach (var orderPosition in from item in cart.OrderPositions
                                          let orderPosition = new OrderPositionDto
                                          {
                                              OrderId = orderDto.Id,
                                              ProductId = item.ProductId,
                                              Amount = item.Amount
                                          }
                                          select orderPosition)
            {
                await orderPositionService.AddAsync(orderPosition);
            }
        }

        private async Task<OrderDto> CreateOrder(CartPoco cart)
        {
            OrderDto order = new OrderDto
            {
                OrderTime = DateTime.Now,
                DeliveryTime = cart.DeliveryTime,
                CustomerName = cart.CustomerName,
                CustomerPhone = cart.CustomerPhone,
                CustomerAddress = cart.CustomerAddress,
                Comment = cart.Comment
            };

            var orderDto = await orderService.AddAsync(order);
            return orderDto;
        }
    }
}
