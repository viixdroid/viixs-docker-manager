using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.Shared.Filters;

public class WrapControllerResultFilter(ILogger<WrapControllerResultFilter> logger) : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        switch (context.Result)
        {
            case ObjectResult { Value: null or ResponseObject }:
                return;
            case ObjectResult objectResult:
            {
                var originalValue = objectResult.Value;
                var responseObject = ResponseObject<object>.Success(originalValue);

                context.Result = new ObjectResult(responseObject)
                {
                    StatusCode = objectResult.StatusCode
                };
                logger.LogInformation("Transformed {OriginalValue} into responseObject: {ResponseObject}", originalValue, responseObject);
                return;
            }
            case StatusCodeResult statusCodeResult:
                var httpStatusCode = statusCodeResult.StatusCode;
                context.Result = new ObjectResult(httpStatusCode is >= 200 and < 300 ? ResponseObject.Success() : ResponseObject.Failure($"{httpStatusCode}"))
                {
                    StatusCode = httpStatusCode
                };
                return;
        }
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}
