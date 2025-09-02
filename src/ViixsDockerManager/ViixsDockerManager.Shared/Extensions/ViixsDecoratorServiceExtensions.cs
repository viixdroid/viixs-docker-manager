using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Decorators;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Shared.Extensions;

public static class ViixsDecoratorServiceExtensions
{
    public static IServiceCollection AddDecoration<TInterface, TImplementation>(
        this IServiceCollection services,
        params Func<TInterface, IServiceProvider, TInterface>[] extraDecorations
    )
        where TInterface : class, IViixsBaseService
        where TImplementation : class, TInterface
    {
        services.AddScoped<TInterface>(serviceProvider =>
        {
            var implementationConstructor = ConstructorCache.GetOrAddConstructorMetadata<TImplementation>();

            var initialObject = implementationConstructor.InvokeConstructor<TImplementation>(serviceProvider);

            TInterface interfaceObject = initialObject;
            if (extraDecorations.Length > 0)
            {
                foreach (var extraDecoration in extraDecorations.Reverse())
                {
                    interfaceObject = extraDecoration(interfaceObject, serviceProvider);
                }
            }

            var logger = serviceProvider.GetRequiredService<ILogger<TInterface>>();
            return ViixsServiceExceptionHandler<TInterface>.CreateService(interfaceObject, logger);
        });
        return services;
    }
}
