using Microsoft.Extensions.DependencyInjection;
using Restaurant.Back.BLL.Models;
using Restaurant.Back.BLL.MsSqlServer.Services;
using Restaurant.Back.BLL.Services.Common;
using Restaurant.Back.DAL.MsSqlServer.Models;
using Restaurant.Back.DAL.MsSqlServer.Repositories;
using Restaurant.Back.Infrastructure.Initializers.Common;
using Restaurant.Back.Repository.Common;

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

            services.AddScoped<ICrudService<OrderDto, int>, OrderService>();
            services.AddScoped<ICrudRepository<Order, int>, OrderRepository>();

            services.AddScoped<ICrudService<OrderPositionDto, int>, OrderPositionService>();
            services.AddScoped<ICrudRepository<OrderPosition, int>, OrderPositionRepository>();

            services.AddScoped<ICrudService<OrderStatusDto, int>, OrderStatusService>();
            services.AddScoped<ICrudRepository<OrderStatus, int>, OrderStatusRepository>();

            services.AddScoped<ICrudService<ProductDto, int>, ProductService>();
            services.AddScoped<ICrudRepository<Product, int>, ProductRepository>();

            services.AddScoped<ICrudService<ProductIngredientDto, int>, ProductIngredientService>();
            services.AddScoped<ICrudRepository<ProductIngredient, int>, ProductIngredientRepository>();

            services.AddScoped<ICrudService<StatusDto, int>, StatusService>();
            services.AddScoped<ICrudRepository<Status, int>, StatusRepository>();
        }
    }
}
