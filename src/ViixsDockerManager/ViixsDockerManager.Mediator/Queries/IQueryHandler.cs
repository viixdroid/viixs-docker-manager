using ViixsDockerManager.Mediator.Handlers;

namespace ViixsDockerManager.Mediator.Queries;

/// <summary>
/// Handles a request for data
/// </summary>
/// <typeparam name="TQuery">The request for data</typeparam>
/// <typeparam name="TResult">The result of the request</typeparam>
public interface IQueryHandler<in TQuery, TResult> : IHandler
    where TQuery : IQuery
{
    /// <summary>
    /// Executes the request for data.
    /// </summary>
    /// <param name="query">
    /// The request to execute
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token that can be used to cancel this request
    /// </param>
    /// <returns>
    /// A Task with a result, meaning this method is async and can be awaited for it's <see cref="TResult">result</see>.
    /// </returns>
    Task<TResult> Execute(TQuery query, CancellationToken cancellationToken = default);
}
