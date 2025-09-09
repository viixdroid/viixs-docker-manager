using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Mediator.Handlers;
using ViixsDockerManager.Mediator.Helpers;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.Mediator.Extensions;

public static class RegisterHandlersServiceExtensions
{
    public static IServiceCollection RegisterCommandHandler<THandler, TCommand>(
        this IServiceCollection services)
        where THandler : class, IHandler
        where TCommand : class, ICommand
    {
        var key = GenericNameHelpers.GetName<TCommand>();
        services.AddKeyedScoped<IHandler, THandler>(key);
        return services;
    }

    public static IServiceCollection RegisterQueryHandler<THandler, TQuery>(
        this IServiceCollection services)
        where THandler : class, IHandler
        where TQuery : class, IQuery
    {
        var key = GenericNameHelpers.GetName<TQuery>();
        services.AddKeyedScoped<IHandler, THandler>(key);
        return services;
    }
}
