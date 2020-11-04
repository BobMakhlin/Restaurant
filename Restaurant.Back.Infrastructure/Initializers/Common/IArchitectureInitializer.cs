using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Back.Infrastructure.Initializers.Common
{
    interface IArchitectureInitializer
    {
        void InitArchitecture(IServiceCollection services);
    }
}
