using Microsoft.Extensions.DependencyInjection;

namespace ViixsDockerManager.Mediator.Extensions;

public static class MediatorServiceExtensions
{
    public static IServiceCollection AddMediatorServices(this IServiceCollection services)
    {
        services.AddSingleton<IMediator, Mediator>();
        return services;
    }
}
