using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using ViixsDockerManager.Mediator.Exceptions;
using ViixsDockerManager.Mediator.Handlers;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.Mediator.Executors;

internal class QueryHandlerExecutor<TResult>
{
    private static readonly Lazy<QueryHandlerExecutor<TResult>> _singletonInstance = new Lazy<QueryHandlerExecutor<TResult>>(() => new QueryHandlerExecutor<TResult>());

    private static readonly ConcurrentDictionary<Type, Func<IHandler, IQuery, CancellationToken, Task<TResult>>> _executeDelegatesCache
        = new ConcurrentDictionary<Type, Func<IHandler, IQuery, CancellationToken, Task<TResult>>>();

    public static QueryHandlerExecutor<TResult> Instance => _singletonInstance.Value;

    private QueryHandlerExecutor()
    {
    }

#pragma warning disable CA1822 //Member 'ExecuteQueryHandler' does not access instance data and can be marked as static
    public Task<TResult> ExecuteQueryHandler(IHandler handler, IQuery query, CancellationToken cancellationToken = default)
#pragma warning restore CA1822 //Member 'ExecuteQueryHandler' does not access instance data and can be marked as static
    {
        var queryType = query.GetType();
        var executor = GetExecuteDelegate(queryType);
        return executor(handler, query, cancellationToken);
    }

    private static Func<IHandler, IQuery, CancellationToken, Task<TResult>> GetExecuteDelegate(Type queryTypeForHandler)
    {
        const string executeMethodName = nameof(IQueryHandler<IQuery, object>.Execute);
        return _executeDelegatesCache.GetOrAdd(queryTypeForHandler, queryType =>
        {
            var resultType = typeof(TResult);

            Type queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, resultType);

            MethodInfo? methodToExecute = queryHandlerType.GetMethod(
                name: executeMethodName,
                bindingAttr: BindingFlags.Public | BindingFlags.Instance,
                binder: null,
                types: [queryType, typeof(CancellationToken)],
                modifiers: null
            );

            if (methodToExecute is null)
            {
                throw new MethodNotFoundException(executeMethodName, queryHandlerType);
            }

            var executingLambda = CreateLambda(methodToExecute, queryHandlerType, queryType);
            return executingLambda.Compile();
        });
    }

    private static Expression<Func<IHandler, IQuery, CancellationToken, Task<TResult>>> CreateLambda(MethodInfo methodToExecute, Type queryHandlerType, Type queryType)
    {
        const string handlerLambdaParameterName = "handler";
        const string queryLambdaParameterName = "query";
        const string cancellationTokenLambdaParameterName = "cancellationToken";

        var handlerLambdaParameter = Expression.Parameter(typeof(IHandler), handlerLambdaParameterName);
        var queryLambdaParameter = Expression.Parameter(typeof(IQuery), queryLambdaParameterName);
        var cancellationTokenParameter = Expression.Parameter(typeof(CancellationToken), cancellationTokenLambdaParameterName);

        var castHandlerToQueryHandler = Expression.Convert(handlerLambdaParameter, queryHandlerType);
        var castToQuery = Expression.Convert(queryLambdaParameter, queryType);

        var callToExecute = Expression.Call(castHandlerToQueryHandler, methodToExecute, castToQuery, cancellationTokenParameter);

        var taskResultForExecute = Expression.Convert(callToExecute, typeof(Task<TResult>));

        return Expression.Lambda<Func<IHandler, IQuery, CancellationToken, Task<TResult>>>(
            taskResultForExecute,
            handlerLambdaParameter,
            queryLambdaParameter,
            cancellationTokenParameter
        );
    }
}
