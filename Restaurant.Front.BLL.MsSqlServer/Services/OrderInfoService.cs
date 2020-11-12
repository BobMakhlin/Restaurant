using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class OrderInfoService : CrudService<OrderInfo, OrderInfoDto, int>
    {
        public OrderInfoService(ICrudRepository<OrderInfo, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<OrderPosition, OrderPositionDto>()
                        .ForMember(item => item.ProductTitle, opt => opt.MapFrom(item => item.Product.Title))
                        .ForMember(item => item.ProductPrice, opt => opt.MapFrom(item => item.Product.Price));

                    ce.CreateMap<OrderPositionDto, OrderPosition>()
                        .ForMember(item => item.Product, opt => opt.Ignore());


                    ce.CreateMap<OrderInfo, OrderInfoDto>();
                    ce.CreateMap<OrderInfoDto, OrderInfo>()
                        .ForMember(item => item.OrderPosition, opt => opt.Ignore());



                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
