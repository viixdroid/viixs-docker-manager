using System.Reflection;
using ViixsDockerManager.Shared.Exceptions;

namespace ViixsDockerManager.Shared.Helpers.Models;

internal sealed record ConstructorMetadata(ConstructorInfo ConstructorInfo, ParameterInfo[] Parameters)
{
    public TResult InvokeConstructor<TResult>(IServiceProvider serviceProvider)
    {
        if (typeof(TResult) != ConstructorInfo.DeclaringType)
        {
            ViixsDockerManagerException.InvalidOperation(
                "Trying to construct a object that is not for the requested type");
        }

        var parameters = ResolveParameters(serviceProvider);
        var result = ConstructorInfo.Invoke(parameters.ToArray());
        return (TResult)result;
    }

    private IEnumerable<object?> ResolveParameters(IServiceProvider serviceProvider)
    {
        return Parameters.Select(p => serviceProvider.GetService(p.ParameterType));
    }
}
