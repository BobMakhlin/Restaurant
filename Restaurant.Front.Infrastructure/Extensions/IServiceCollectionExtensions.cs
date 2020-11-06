using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Front.Infrastructure.Initializers.Common;
using Restaurant.Back.Infrastructure.Initializers.Custom.MsSqlServer;

namespace Restaurant.Front.Infrastructure.Extensions
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
