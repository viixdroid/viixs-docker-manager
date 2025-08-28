namespace ViixsDockerManager.Shared.Models;

public record ResponseObject(bool IsSuccess, IEnumerable<string>? Errors = null) : IResponseObject
{
    public static IResponseObject Success() => new ResponseObject(true);

    public static IResponseObject Failure(string errorMessage) =>
        new ResponseObject(false, [errorMessage]);

    public static IResponseObject Failure(IEnumerable<string> errorMessages) =>
        new ResponseObject(false, errorMessages);
}

public record ResponseObject<TResult>(TResult Result, bool IsSuccess, IEnumerable<string>? Errors = null)
    : ResponseObject(IsSuccess, Errors), IResponseObject<TResult>
{
    public static IResponseObject<TResult> Success(TResult result) =>
        new ResponseObject<TResult>(result, true);
}
