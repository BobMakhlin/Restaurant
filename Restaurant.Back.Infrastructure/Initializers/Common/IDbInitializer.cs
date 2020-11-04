using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Back.Infrastructure.Initializers.Common
{
    interface IDbInitializer
    {
        void InitDb(IServiceCollection services);
    }
}
