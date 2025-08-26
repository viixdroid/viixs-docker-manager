using System.Reflection;
using Microsoft.Extensions.Logging;
using ViixsDockerManager.Shared.Exceptions;
using ViixsDockerManager.Shared.Helpers;

namespace ViixsDockerManager.Shared.Decorators;

public class ViixsServiceExceptionHandler<TService> : DispatchProxy
{
    private TService _service;
    private ILogger<TService> _logger;

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
                return targetMethod.Invoke(_service, args);
            }

            if (returnType == typeof(Task))
            {
                return targetMethod.AsAsync(_service, args, WrapException);
            }

            return targetMethod.AsAsyncWithResult(_service, args, WrapException);
        }
        catch (Exception exception)
            when (exception is TargetInvocationException or AggregateException)
        {
            WrapException(targetMethod, exception);
            return null; //Should have already thrown here.
        }
    }

    private void WrapException(MethodInfo? methodInfo, Exception exception)
    {
        var innerException = exception.InnerException ?? exception;

        _logger.LogError(innerException,
            "The service method {Service}::{Method} threw and exception with message: {ExceptionMessage}",
            typeof(TService).Name, methodInfo?.Name ?? "Unknown Method", exception.Message);

        throw ViixsDockerManagerException.ServiceException(innerException.Message, innerException);
    }
}
