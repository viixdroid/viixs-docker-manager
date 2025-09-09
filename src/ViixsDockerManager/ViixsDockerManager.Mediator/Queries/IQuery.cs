namespace ViixsDockerManager.Mediator.Queries;


/// <summary>
/// Represents a request for data resulting in <see cref="TResult" />
/// </summary>
public interface IQuery {}
public interface IQuery<out TResult> : IQuery
{

}
