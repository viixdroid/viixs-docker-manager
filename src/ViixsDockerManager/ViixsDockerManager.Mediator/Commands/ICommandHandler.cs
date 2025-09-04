using ViixsDockerManager.Mediator.Handlers;

namespace ViixsDockerManager.Mediator.Commands;

/// <summary>
/// Handles a <see cref="TCommand" /> for changing state
/// </summary>
/// <typeparam name="TCommand">The actual command to handle</typeparam>
public interface ICommandHandler<in TCommand> : IHandler
    where TCommand : ICommand
{
    /// <summary>
    /// Handles a given command
    /// </summary>
    /// <param name="command">The command to handle</param>
    /// <param name="cancellationToken">
    /// Cancellation token that can be used to cancel this request
    /// </param>
    /// <returns>
    /// This returns a task object, meaning this method is async.
    /// </returns>
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}
