using HR.Infrastructure.Initializers.Custom.MsSqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using HR.Infrastructure.Initializers.Custom.MySql;


namespace Restaurant.Back.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void InitArchitecture(this IServiceCollection services)
        {
            IArchitectureInitializer initializer = new ArchitectureInitializer();

            initializer.InitArchitecture(services);
        }

        public static void InitDb(this IServiceCollection services, IConfiguration config)
        {
            IDbInitializer initializer = new DbInitializer(config);

            initializer.InitDb(services);
        }
    }
}
