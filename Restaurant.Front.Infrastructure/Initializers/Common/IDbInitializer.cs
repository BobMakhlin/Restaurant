using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Front.Infrastructure.Initializers.Common
{
    interface IDbInitializer
    {
        void InitDb(IServiceCollection services);
    }
}
