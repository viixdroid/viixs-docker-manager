
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Mediator.Exceptions;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Mediator.Commands;

public abstract class CommandHandlerBase<TCommand, TCommandNotificationSender>(TCommandNotificationSender commandNotificationSender, ILogger logger)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
    where TCommandNotificationSender : ICommandNotificationSender
{
    Task ICommandHandler<TCommand>.Handle(TCommand command, CancellationToken cancellationToken) => Handle(command, cancellationToken);

    protected virtual Task SendCommandNotificationAfterCommandAsync(TCommandNotificationSender commandNotificationSender, TCommand command, bool executeCommandResult, CancellationToken cancellationToken = default) => Task.CompletedTask;
    protected virtual Task SendCommandNotificationBeforeCommandAsync(TCommandNotificationSender commandNotificationSender, TCommand command, CancellationToken cancellationToken = default) => Task.CompletedTask;

    protected abstract Task<bool> ExecuteCommandAsync(TCommand command, CancellationToken cancellationToken = default);

    private async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        await SendCommandNotificationBeforeCommandAsync(commandNotificationSender, command, cancellationToken).ConfigureAwait(false);
        bool executeCommandResult = false;
        try
        {
            executeCommandResult = await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            if (e is ViixDockerManagerWithHttpStatusCodeException vdmException)
            {
                vdmException.Log(logger);
                throw;
            }

            var handlerException = new CommandHandlerException(e.Message, e);
            handlerException.Log(logger);
            throw handlerException;
        }
        finally
        {
            await SendCommandNotificationAfterCommandAsync(commandNotificationSender, command, executeCommandResult, cancellationToken).ConfigureAwait(false);
        }
    }
}
