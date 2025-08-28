using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Decorators;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Shared.Extensions;

public static class ViixsDecoratorServiceExtensions
{
    public static IServiceCollection AddDecoration<TInterface, TImplementation>(this IServiceCollection services)
        where TInterface : class, IViixsBaseService
        where TImplementation : class, TInterface
    {
        services.AddScoped<TInterface>(serviceProvider =>
        {
            var implementationConstructor = ConstructorCache.GetOrAddConstructorMetadata<TImplementation>();

            var implementation = implementationConstructor.InvokeConstructor<TImplementation>(serviceProvider);

            var logger = serviceProvider.GetRequiredService<ILogger<TInterface>>();
            return ViixsServiceExceptionHandler<TInterface>.CreateService(implementation, logger);
        });
        return services;
    }
}
