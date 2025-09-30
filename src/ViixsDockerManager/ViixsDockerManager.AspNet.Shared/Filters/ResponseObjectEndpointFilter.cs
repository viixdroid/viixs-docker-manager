using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using ViixsDockerManager.Shared.Models;

namespace ViixsDockerManager.Shared.AspNet.Filters;

public class ResponseObjectEndpointFilter : IEndpointFilter
{
    public ResponseObjectEndpointFilter()
    {
        
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        if (result is IValueHttpResult valueResult)
        {
            var originalValue = valueResult.Value;
            var wrappedResult = WrapResultInResponseObject(originalValue);

            return Results.Json(wrappedResult, statusCode: StatusCodes.Status200OK);
        }

        if (result is not IStatusCodeHttpResult { StatusCode: not null } statusCodeResult)
        {
            if (result is EmptyHttpResult)
            {
                return Results.Json(ResponseObject.Success(), statusCode: StatusCodes.Status200OK);
            }

            var wrappedValue = WrapResultInResponseObject(result);
            return Results.Json(wrappedValue, statusCode: StatusCodes.Status200OK);
        }

        var statusCode = statusCodeResult.StatusCode.Value;

        var responseObject = statusCode is >= 200 and < 300
            ? ResponseObject.Success()
            : ResponseObject.Failure($"{statusCode}");
        return Results.Json(responseObject, statusCode: statusCode);

        IResponseObject WrapResultInResponseObject(object? resultValue)
        {
            if (resultValue is null)
            {
                return ResponseObject.Failure("no result");
            }

            Type originalValueType = resultValue.GetType();

            Type genericResultClassType = typeof(ResponseObject<>).MakeGenericType(originalValueType);

            MethodInfo? successMethod = genericResultClassType.GetMethod(
                nameof(ResponseObject.Success),
                BindingFlags.Public | BindingFlags.Static,
                null,
                [originalValueType],
                null
            );
            if (successMethod == null)
            {
                return ResponseObject.Failure("Could not find correct method for creating response");
            }

            var wrappedResult = successMethod.Invoke(null, [resultValue!]) as ResponseObject;
            return wrappedResult ?? ResponseObject.Failure("Could not create a response object");
        }
    }
}
