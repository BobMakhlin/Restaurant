using Microsoft.Extensions.DependencyInjection;

namespace Restaurant.Front.Infrastructure.Initializers.Common
{
    interface IArchitectureInitializer
    {
        void InitArchitecture(IServiceCollection services);
    }
}
