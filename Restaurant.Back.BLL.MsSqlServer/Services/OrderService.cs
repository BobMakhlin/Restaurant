
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

                    
                    ce.CreateMap<OrderPosition, OrderPositionDto>()
                        .ForMember(item => item.ProductTitle, opt => opt.MapFrom(item => item.Product.Title))
                        .ForMember(item => item.ProductPrice, opt => opt.MapFrom(item => item.Product.Price));

                    ce.CreateMap<OrderPositionDto, OrderPosition>()
                        .ForMember(item => item.Product, opt => opt.Ignore());


                    ce.CreateMap<Order, OrderDto>()
                        .ForMember(item => item.Statuses, opt => opt.MapFrom
                            (
                                p => p.OrderStatus.Select(pi => pi.Status)
                            ));
                    ce.CreateMap<OrderDto, Order>()
                        .ForMember(item => item.OrderPosition, opt => opt.Ignore());



                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
