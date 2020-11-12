using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Front.Api.Helpers
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
                item.OrderInfoId = orderId;

                await orderPositionService.AddAsync(item);
            }
        }

        public async Task DeleteOrderPositionAsync(int orderId)
        {
            var orderPositions = await orderPositionService
                .Where(op => op.OrderInfoId == orderId)
                .ToListAsync();

            foreach (var orderPosition in orderPositions)
            {
                await orderPositionService.DeleteAsync(orderPosition);
            }
        }
    }
}
