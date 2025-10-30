using ViixsDockerManager.Mediator.Commands;
using ViixsDockerManager.Mediator.Queries;

namespace ViixsDockerManager.Mediator;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ConTestAttribute<TConType> : Attribute
    where TConType : class
{
    public ConTestAttribute()
    {
        var conTypeType = typeof(TConType);
        if (!typeof(IQuery).IsAssignableFrom(conTypeType)
            && !typeof(ICommand).IsAssignableFrom(conTypeType))
        {
            throw new ArgumentException("TConType must implement either IQuery or ICommand interface.");
        }
        ConTypeName = conTypeType.Name;
    }

    public string ConTypeName { get; }
    public string ControllerName { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty;
    public HttpMethod HttpMethod { get; init; } = HttpMethod.Get;
}
