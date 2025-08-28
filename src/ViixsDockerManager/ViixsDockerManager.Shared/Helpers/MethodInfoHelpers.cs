using System.Reflection;

namespace ViixsDockerManager.Shared.Helpers;

internal static class MethodInfoHelpers
{
    public static async Task AsAsync<TService>(this MethodInfo targetMethod, TService service, object?[]? arguments,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        try
        {
            var task = (Task)targetMethod.Invoke(service, arguments)!;
            await task.ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            onException?.Invoke(targetMethod, exception);
        }
    }

    public static object? AsAsyncWithResult<TService>(this MethodInfo targetMethod, TService service,
        object?[]? arguments,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        var returnType = targetMethod.ReturnType;
        var resultType = returnType.GetGenericArguments()[0];
        var asyncHandleMethod = typeof(MethodInfoHelpers).GetMethod(nameof(ExecuteAsyncCallWithResult), BindingFlags.Static | BindingFlags.NonPublic);
        asyncHandleMethod = Guard.ValueIsNotNull(asyncHandleMethod, nameof(asyncHandleMethod));
        var asyncGenericMethod = asyncHandleMethod.MakeGenericMethod(typeof(TService), resultType);
        return asyncGenericMethod.Invoke(null, [targetMethod, service, arguments, onException]);
    }

    private static async Task<TResult> ExecuteAsyncCallWithResult<TService, TResult>(this MethodInfo targetMethod,
        TService service, object?[]? arguments,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        try
        {
            var task = (Task<TResult>)targetMethod.Invoke(service, arguments)!;
            return await task.ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            onException?.Invoke(targetMethod, exception);
            return default!;
        }
    }
}
