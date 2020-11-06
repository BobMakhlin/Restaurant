using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Front.BLL.Models;
using Restaurant.Front.BLL.MsSqlServer.Services;
using Restaurant.Front.BLL.Services.Common;
using Restaurant.Front.DAL.MsSqlServer.Models;
using Restaurant.Front.DAL.MsSqlServer.Repositories;
using Restaurant.Front.Infrastructure.Initializers.Common;
using Restaurant.Front.Repository.Common;

namespace Restaurant.Back.Infrastructure.Initializers.Custom.MsSqlServer
{
    class ArchitectureInitializer : IArchitectureInitializer
    {
        public void InitArchitecture(IServiceCollection services)
        {
            services.AddScoped<ICrudService<CategoryDto, int>, CategoryService>();
            services.AddScoped<ICrudRepository<Category, int>, CategoryRepository>();

            services.AddScoped<ICrudService<IngredientDto, int>, IngredientService>();
            services.AddScoped<ICrudRepository<Ingredient, int>, IngredientRepository>();

            services.AddScoped<ICrudService<LabelDto, int>, LabelService>();
            services.AddScoped<ICrudRepository<Label, int>, LabelRepository>();

            services.AddScoped<ICrudService<ProductIngredientDto, int>, ProductIngredientService>();
            services.AddScoped<ICrudRepository<ProductIngredient, int>, ProductIngredientRepository>();

            services.AddScoped<ICrudService<ProductLabelDto, int>, ProductLabelService>();
            services.AddScoped<ICrudRepository<ProductLabel, int>, ProductLabelRepository>();

            services.AddScoped<ICrudService<ProductDto, int>, ProductService>();
            services.AddScoped<ICrudRepository<Product, int>, ProductRepository>();
        }
    }
}
