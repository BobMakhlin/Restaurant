using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Helpers
{
    public class OrderPositionHelper
    {
        ICrudService<OrderPositionDto, int> orderPositionService;
        public OrderPositionHelper(ICrudService<OrderPositionDto, int> orderPositionService)
        {
            this.orderPositionService = orderPositionService;
        }

        public async Task AddOrderPositionAsync(int orderId, IEnumerable<OrderPositionDto> orderPositionDtos)
        {
            foreach (var item in orderPositionDtos)
            {
                var position = new OrderPositionDto
                {
                    OrderId = orderId,
                    Amount = item.Amount,
                    ProductId = item.ProductId
                };


                await orderPositionService.AddAsync(position);
            }
        }

        public async Task DeleteOrderPositionAsync(int orderId)
        {
            var orderPositions = await orderPositionService.Where(op => op.OrderId == orderId).ToArrayAsync();

            foreach (var orderPosition in orderPositions)
            {
                await orderPositionService.DeleteAsync(orderPosition);
            }
        }
    }
}
