using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Front.BLL.MsSqlServer.Services
{
    public class ProductService : CrudService<Product, ProductDto, int>
    {
        public ProductService(ICrudRepository<Product, int> repository) : base(repository)
        {
        }

        protected override IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration
            (
                ce =>
                {
                    ce.CreateMap<Category, CategoryDto>();
                    ce.CreateMap<Ingredient, IngredientDto>();
                    ce.CreateMap<Label, LabelDto>();


                    ce.CreateMap<Product, ProductDto>()
                        .ForMember
                            (
                                item => item.Ingredients,
                                opt => opt.MapFrom
                                (
                                    p => p.ProductIngredient.Select(item => item.Ingredient)
                                )
                            )
                        .ForMember
                            (
                                item => item.Labels,
                                opt => opt.MapFrom
                                (
                                    p => p.ProductLabel.Select(item => item.Label)
                                )
                            );

                    ce.CreateMap<ProductDto, Product>();


                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
