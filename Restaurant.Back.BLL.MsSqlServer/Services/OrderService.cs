
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Back.BLL.MsSqlServer.Services
{
    public class OrderService : CrudService<Order, OrderDto, int>
    {
        public OrderService(ICrudRepository<Order, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<Status, StatusDto>().ReverseMap();                    
                    ce.CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
                    ce.CreateMap<Order, OrderDto>()
                        .ForMember(item => item.Statuses, opt => opt.MapFrom
                            (
                                p => p.OrderStatus.Select(pi => pi.Status)
                            ));
                    ce.CreateMap<OrderDto, Order>();

                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
