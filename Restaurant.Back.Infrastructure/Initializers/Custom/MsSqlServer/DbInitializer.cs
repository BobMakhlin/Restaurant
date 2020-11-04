using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Back.DAL.MsSqlServer.Data;
using Restaurant.Back.Infrastructure.Initializers.Common;

namespace Restaurant.Back.Infrastructure.Initializers.Custom.MsSqlServer
{
    class DbInitializer : IDbInitializer
    {
        IConfiguration m_configuration;

        public DbInitializer(IConfiguration configuration)
        {
            m_configuration = configuration;
        }


        public void InitDb(IServiceCollection services)
        {
            services.AddScoped<DbContext, RestaurantBackContext>();


            var conString = m_configuration.GetConnectionString("MsSqlServer");
            services.AddDbContext<RestaurantBackContext>(opt =>
            {
                opt.UseSqlServer(conString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
