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

        public async Task AddStatusesAsync(int orderId, IEnumerable<StatusDto> statuses)
        {
            foreach(var status in statuses)
            {
                var orderStatus = new OrderStatusDto
                {
                    OrderId = orderId,
                    StatusId = status.Id
                };

                await orderStatusService.AddAsync(orderStatus);
            }
        }

        public async Task DeleteStatusesAsync(int orderId)
        {
            var orderStatus = await orderStatusService.Where(item => item.OrderId == orderId).ToListAsync();
            
            foreach(var status in orderStatus)
            {
                await orderStatusService.DeleteAsync(status);
            }
        }
    }
}
