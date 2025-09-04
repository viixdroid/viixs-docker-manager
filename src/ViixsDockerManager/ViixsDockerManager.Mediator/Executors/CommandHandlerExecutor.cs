using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Mediator.Exceptions;
using ViixsDockerManager.Mediator.Handlers;

namespace ViixsDockerManager.Mediator.Executors;

internal class CommandHandlerExecutor
{
    private static readonly Lazy<CommandHandlerExecutor> _singletonInstance = new Lazy<CommandHandlerExecutor>(() => new CommandHandlerExecutor());

    private static readonly ConcurrentDictionary<Type, Func<IHandler, ICommand, CancellationToken, Task>> _handleDelegatesCache = new ConcurrentDictionary<Type, Func<IHandler, ICommand, CancellationToken, Task>>();

    public static CommandHandlerExecutor Instance => _singletonInstance.Value;

    private CommandHandlerExecutor() { }

    public Task ExecuteCommandHandler(IHandler handler, ICommand command, CancellationToken cancellationToken = default)
    {
        var commandType = command.GetType();
        var executor = GetHandleDelegate(commandType);
        return executor(handler, command, cancellationToken);
    }

    private static Func<IHandler, ICommand, CancellationToken, Task> GetHandleDelegate(Type commandTypeForHandler)
    {
        const string handleMethodName = nameof(ICommandHandler<ICommand>.Handle);

        return _handleDelegatesCache.GetOrAdd(commandTypeForHandler, commandType =>
        {
            Type commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);

            MethodInfo? handleMethodToExecute = commandHandlerType.GetMethod(
                name: handleMethodName,
                bindingAttr: BindingFlags.Public | BindingFlags.Instance,
                binder: null,
                types: [commandType, typeof(CancellationToken)],
                modifiers: null
            );

            if (handleMethodToExecute is null)
            {
                throw new MethodNotFoundException(handleMethodName, commandType);
            }

            var executingLambda = CreateLambda(handleMethodToExecute, commandHandlerType, commandType);
            return executingLambda.Compile();
        });
    }

    private static Expression<Func<IHandler, ICommand, CancellationToken, Task>> CreateLambda(MethodInfo handleMethodToExecute, Type commandHandlerType, Type commandType)
    {
        const string handlerLambdaParameterName = "handler";
        const string commandLambdaParameterName = "command";
        const string cancellationTokenLambdaParameterName = "cancellationToken";

        var handlerLambdaParameter = Expression.Parameter(typeof(IHandler), handlerLambdaParameterName);
        var commandLambdaParameter = Expression.Parameter(typeof(ICommand), commandLambdaParameterName);
        var cancellationTokenLambdaParameter = Expression.Parameter(typeof(CancellationToken), cancellationTokenLambdaParameterName);

        var castHandlerToCommandHandler = Expression.Convert(handlerLambdaParameter, commandHandlerType);
        var castToCommand = Expression.Convert(commandLambdaParameter, commandType);

        var handleCallToExecute = Expression.Call(castHandlerToCommandHandler, handleMethodToExecute, castToCommand, cancellationTokenLambdaParameter);

        var taskForHandler = Expression.Convert(handleCallToExecute, typeof(Task));

        return Expression.Lambda<Func<IHandler, ICommand, CancellationToken, Task>>(
            taskForHandler,
            handlerLambdaParameter,
            commandLambdaParameter,
            cancellationTokenLambdaParameter
        );
    }
}
