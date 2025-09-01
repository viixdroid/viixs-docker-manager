using System.Reflection;
using ViixsDockerManager.Shared.Services;

namespace ViixsDockerManager.Shared.Helpers;

internal static class MethodInfoHelpers
{
    /// <summary>
    /// Runs a given method on a service as a Task. This means we can await it, assuming the methodinfo returns a task.
    ///
    /// </summary>
    /// <param name="targetMethod">
    /// Which method to run async
    /// </param>
    /// <param name="service">
    /// The service object which has the <see cref="targetMethod"/>
    /// </param>
    /// <param name="arguments">
    /// An array of objects which represents the arguments needed for the method to run
    /// </param>
    /// <param name="onBefore">
    /// A callback that happens before a execute the <see cref= "targetMethod" />
    /// </param>
    /// <param name="onException">
    /// A callback that happens when there is an exception while running the <see cref="targetMethod"/>.
    ///
    /// In this callback you can handle any of the exceptions thrown
    /// </param>
    /// <typeparam name="TService">
    /// The service type which contains the <see cref="targetMethod"/>
    /// </typeparam>
    public static async Task InvokeAsAsync<TService>(this MethodInfo targetMethod, TService service,
        object?[]? arguments,
        Func<MethodInfo, object?[]?, Task>? onBefore = null,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        try
        {
            if (onBefore is not null)
            {
                await onBefore.Invoke(targetMethod, arguments);
            }

            var task = (Task)targetMethod.Invoke(service, arguments)!;
            await task.ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            onException?.Invoke(targetMethod, exception);
        }
    }

    /// <summary>
    /// Runs a given method on a service as a Task with a return value. This means we can await it, assuming the methodinfo returns a task with a return value.
    ///
    /// This method will also return the result.
    /// </summary>
    /// <param name="targetMethod">
    /// Which method to run async
    /// </param>
    /// <param name="service">
    /// The service object which has the <see cref="targetMethod"/>
    /// </param>
    /// <param name="arguments">
    /// An array of objects which represents the arguments needed for the method to run
    /// </param>
    /// <param name="onBefore">
    /// A callback that happens before a execute the <see cref= "targetMethod" />
    /// </param>
    /// <param name="onException">
    /// A callback that happens when there is an exception while running the <see cref="targetMethod"/>.
    ///
    /// In this callback you can handle any of the exceptions thrown
    /// </param>
    /// <typeparam name="TService">
    /// The service type which contains the <see cref="targetMethod"/>
    /// </typeparam>
    /// <returns>
    /// The result from running <see cref="targetMethod"/>, assuming there has been no exception.
    /// </returns>
    public static object? InvokeAsAsyncWithResult<TService>(this MethodInfo targetMethod, TService service,
        object?[]? arguments,
        Func<MethodInfo, object?[]?, Task>? onBefore = null,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        var returnType = targetMethod.ReturnType;
        var resultType = returnType.GetGenericArguments()[0];
        var asyncHandleMethod = typeof(MethodInfoHelpers).GetMethod(nameof(ExecuteAsyncCallWithResult),
            BindingFlags.Static | BindingFlags.NonPublic);
        asyncHandleMethod = Guard.ValueIsNotNull(asyncHandleMethod, nameof(asyncHandleMethod));
        var asyncGenericMethod = asyncHandleMethod.MakeGenericMethod(typeof(TService), resultType);
        return asyncGenericMethod.Invoke(null, [targetMethod, service, arguments, onBefore, onException]);
    }

    private static async Task<TResult> ExecuteAsyncCallWithResult<TService, TResult>(this MethodInfo targetMethod,
        TService service, object?[]? arguments,
        Func<MethodInfo, object?[]?, Task>? onBefore = null,
        Action<MethodInfo?, Exception>? onException = null)
    {
        targetMethod = Guard.ValueIsNotNull(targetMethod, nameof(targetMethod));
        try
        {
            if (onBefore is not null)
            {
                await onBefore.Invoke(targetMethod, arguments);
            }

            var task = (Task<TResult>)targetMethod.Invoke(service, arguments)!;
            return await task.ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            onException?.Invoke(targetMethod, exception);
            return default!;
        }
    }

    /// <summary>
    /// Invokes a sync method
    /// </summary>
    /// <param name="targetMethod">The method to invoke</param>
    /// <param name="service">The service object which has the <see cref="targetMethod"/></param>
    /// <param name="arguments"> An array of objects which represents the arguments needed for the method to run</param>
    /// <param name="onBefore">A callback that happens before a execute the <see cref= "targetMethod" /></param>
    /// <param name="onException">
    /// A callback that happens when there is an exception while running the <see cref="targetMethod"/>.
    ///
    /// In this callback you can handle any of the exceptions thrown
    /// </param>
    /// <typeparam name="TService">
    /// The service type which contains the <see cref="targetMethod"/>
    /// </typeparam>
    /// <returns>
    /// The result from running <see cref="targetMethod"/>, assuming there has been no exception.
    /// </returns>
    public static object? InvokeSync<TService>(this MethodInfo targetMethod, TService service, object?[]? arguments,
        Action<MethodInfo, object?[]?>? onBefore = null, Action<MethodInfo?, Exception>? onException = null)
    {
        try
        {
            onBefore?.Invoke(targetMethod, arguments);

            return targetMethod.Invoke(service, arguments);
        }
        catch (Exception exception)
        {
            onException?.Invoke(targetMethod, exception);
            return null!;
        }
    }
}
