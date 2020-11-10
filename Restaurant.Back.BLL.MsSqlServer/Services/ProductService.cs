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
                    ce.CreateMap<Ingredient, IngredientDto>().ReverseMap();
                    ce.CreateMap<Category, CategoryDto>().ReverseMap();
                    ce.CreateMap<ProductIngredient, ProductIngredientDto>().ReverseMap();


                    ce.CreateMap<Product, ProductDto>()
                        .ForMember
                        (
                            item => item.Ingredients,
                            opt => opt.MapFrom
                            (
                                p => p.ProductIngredient.Select(pi => pi.Ingredient)
                            )
                        );

                    ce.CreateMap<ProductDto, Product>()
                        .ForMember(item => item.Category, opt => opt.Ignore())

                        .ForMember
                        (
                            item => item.CategoryId,
                            opt => opt.MapFrom
                            (
                                p => p.Category.Id
                            )
                        );



                    ce.AddExpressionMapping();
                }
            );
            return new Mapper(mapperConfig);
        }
    }
}
