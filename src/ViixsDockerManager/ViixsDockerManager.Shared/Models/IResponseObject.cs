namespace ViixsDockerManager.Shared.Models;

public interface IResponseObject
{
    bool IsSuccess { get; }
    IEnumerable<string>? Errors { get; }
}

public interface IResponseObject<out TResult> : IResponseObject
{
    TResult Result { get; }
}
