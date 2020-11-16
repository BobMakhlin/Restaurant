using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.Services.Common;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Back.BLL.MsSqlServer.Services
{
    public class OrderStatusService : CrudService<OrderStatus, OrderStatusDto, int>
    {
        public OrderStatusService(ICrudRepository<OrderStatus, int> repository) : base(repository)
        {
        }
        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<OrderStatus, OrderStatusDto>();
                    ce.CreateMap<OrderStatusDto, OrderStatus>()
                        .ForMember(item => item.Status, opt => opt.Ignore());

                    ce.CreateMap<Product, ProductDto>();

                    ce.AddExpressionMapping();
                }
            );

            return new Mapper(mapperConfig);
        }
    }
}
