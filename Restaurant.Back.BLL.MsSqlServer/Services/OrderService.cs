
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
                    ce.CreateMap<OrderStatus, OrderStatusDto>()
                        .ForMember(item => item.StatusTitle, opt => opt.MapFrom(item => item.Status.Title));
                    ce.CreateMap<OrderStatusDto, OrderStatus>()
                        .ForMember(item => item.Status, opt => opt.Ignore());

                    ce.CreateMap<OrderPosition, OrderPositionDto>()
                        .ForMember(item => item.ProductTitle, opt => opt.MapFrom(item => item.Product.Title))
                        .ForMember(item => item.ProductPrice, opt => opt.MapFrom(item => item.Product.Price));
                    ce.CreateMap<OrderPositionDto, OrderPosition>()
                        .ForMember(item => item.Product, opt => opt.Ignore())
                        .ForMember(item => item.Id, opt => opt.Ignore());

                    ce.CreateMap<Order, OrderDto>();
                    ce.CreateMap<OrderDto, Order>()
                        .ForMember(item => item.OrderPosition, opt => opt.Ignore())
                        .ForMember(item => item.OrderStatus, opt => opt.Ignore());

                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
