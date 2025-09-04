using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Mediator.Handlers;
using ViixsDockerManager.Mediator.Helpers;
using ViixsDockerManager.Mediator.Queries;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Mediator.Extensions;

internal static class ServiceProviderExtensions
{
    public static IHandler ResolveCommandHandler(
        this IServiceProvider serviceProvider,
        Type commandType)
    {
        var key = GenericNameHelpers.GetName(commandType);
        return serviceProvider.GetRequiredKeyedService<IHandler>(key);
    }

    public static IHandler ResolveQueryHandler(
        this IServiceProvider serviceProvider,
        Type queryType)
    {
        var key = GenericNameHelpers.GetName(queryType);
        return serviceProvider.GetRequiredKeyedService<IHandler>(key);
    }
}
