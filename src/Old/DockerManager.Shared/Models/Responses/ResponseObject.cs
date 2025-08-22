namespace DockerManager.Shared.Models.Responses;

public record ResponseObject(bool IsSuccess, IEnumerable<string>? Errors = null)
{
    public static ResponseObject Success() => new ResponseObject(true);

    public static ResponseObject Failure(string errorMessage) =>
        new ResponseObject(false, [errorMessage]);

    public static ResponseObject Failure(IEnumerable<string> errorMessages) =>
        new ResponseObject(false, errorMessages);
}

public record ResponseObject<TResult>(TResult Result, bool IsSuccess, IEnumerable<string>? Errors = null)
    : ResponseObject(IsSuccess, Errors)
{
    public static ResponseObject<TResult> Success(TResult result) =>
        new ResponseObject<TResult>(result, true);
}
