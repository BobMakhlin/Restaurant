using Microsoft.EntityFrameworkCore;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Back.Api.Helpers
{
    public class OrderStatusHelper
    {
        ICrudService<OrderStatusDto, int> orderStatusService;
        public OrderStatusHelper(ICrudService<OrderStatusDto, int> orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        public async Task AddOrderStatusAsync(int orderId, IEnumerable<OrderStatusDto> orderStatusDtos)
        {
            foreach (var item in orderStatusDtos)
            {
                var status = new OrderStatusDto
                {
                    OrderId = orderId,
                    Time = item.Time,
                    StatusId = item.StatusId
                };
                await orderStatusService.AddAsync(status);
            }
        }

        public async Task DeleteOrderStatusAsync(int orderId)
        {
            var orderStatus = await orderStatusService.Where(item => item.OrderId == orderId).ToListAsync();

            foreach (var status in orderStatus)
            {
                await orderStatusService.DeleteAsync(status);
            }
        }
    }
}
