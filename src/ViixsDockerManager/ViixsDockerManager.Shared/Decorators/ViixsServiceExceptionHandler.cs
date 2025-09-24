using System.Reflection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Shared.Decorators;

public class ViixsServiceExceptionHandler<TService> : DispatchProxy
{
    private TService? _service;
    private ILogger<TService>? _logger;

    public static TService CreateService(TService service, ILogger<TService> logger)
    {
        var proxy = Create<TService, ViixsServiceExceptionHandler<TService>>();
        if (proxy is ViixsServiceExceptionHandler<TService> serviceExceptionHandler)
        {
            serviceExceptionHandler.SetParameters(service, logger);
        }

        return proxy;
    }

    private void SetParameters(TService service,
        ILogger<TService> logger)
    {
        _logger = Guard.ValueIsNotNull(logger, nameof(logger));
        _service = Guard.ValueIsNotNull(service, nameof(service));
    }

    protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
    {
        _ = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        try
        {
            var returnType = targetMethod!.ReturnType;

            if (!typeof(Task).IsAssignableFrom(returnType))
            {
                return targetMethod.InvokeSync(_service, args, onException: WrapException);
            }

            if (returnType == typeof(Task))
            {
                return targetMethod.InvokeAsAsync(_service, args, onException: WrapException);
            }

            return targetMethod.InvokeAsAsyncWithResult(_service, args, onException: WrapException);
        }
        catch (Exception exception)
        {
            WrapException(targetMethod, exception);
            return null; //Should have already thrown here.
        }
    }

    private void WrapException(MethodInfo? methodInfo, Exception exception)
    {
        if (exception is ViixsDockerManagerWithHttpStatusCodeException viixsDockerManagerWithHttpStatusCodeException)
        {
            viixsDockerManagerWithHttpStatusCodeException.Log(_logger!);
            throw viixsDockerManagerWithHttpStatusCodeException;
        }

        var innerException = exception.InnerException ?? exception;
        //The inner exception can also be a custom exception.
        if (innerException is ViixsDockerManagerWithHttpStatusCodeException viixsDockerManagerWithHttpStatusCodeException1)
        {
            viixsDockerManagerWithHttpStatusCodeException1.Log(_logger!);
            throw viixsDockerManagerWithHttpStatusCodeException1;
        }

        var serviceFaultedException = new ServiceFaultUnhandledException(typeof(TService).Name, methodInfo?.Name ?? "Unknown Method", innerException);
        serviceFaultedException.Log(_logger!);
        throw serviceFaultedException;
    }
}
