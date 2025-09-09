using System.Reflection;
using ViixsDockerManager.Mediator.Commands;
using Microsoft.Extensions.DependencyInjection;
using ViixsDockerManager.Mediator.Executors;
using ViixsDockerManager.Mediator.Extensions;
using ViixsDockerManager.Mediator.Handlers;
using ViixsDockerManager.Mediator.Helpers;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        var actualCommandType = command.GetType();

        var handlerExecutor = CommandHandlerExecutor.Instance;

        var scoped = serviceProvider.CreateScope();

        var handler = scoped.ServiceProvider.ResolveCommandHandler(actualCommandType);

        return handlerExecutor.ExecuteCommandHandler(handler, command, cancellationToken);
    }

    public async Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var actualQueryType = query.GetType();

        var executor = QueryHandlerExecutor<TResult>.Instance;

        var scoped = serviceProvider.CreateScope();

        var handler = scoped.ServiceProvider.ResolveQueryHandler(actualQueryType);

        var result = await executor.ExecuteQueryHandler(handler, query, cancellationToken).ConfigureAwait(false);

        return result;
    }
}
