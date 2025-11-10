using System.Diagnostics.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Constants;

internal static class ViixsControllerAttributeDefinition
{
    
    public const string FileName = "ViixsControllerAttribute.g.cs";
    public const string FullTypeName = "ViixsDockerManager.Shared.Attributes.ViixsControllerAttribute";
    //TODO: Move to file and read from there. Or embed as resource.
    public const string ViixControllerAttributeText = """
        using System;
        using Microsoft.CodeAnalysis;
        //using ViixsDockerManager.Mediator.Commands;
        //using ViixsDockerManager.Mediator.Queries;

        namespace ViixsDockerManager.Shared.Attributes
        {        
            [Embedded]
            [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
            public class ViixsControllerAttribute : Attribute           
            {
                public ViixsControllerAttribute(Type queryOrCommandType)
                {                
                    //if (!typeof(IQuery).IsAssignableFrom(queryOrCommandType)
                    //    && !typeof(ICommand).IsAssignableFrom(queryOrCommandType))
                    //{
                    //    throw new ArgumentException($"nameof(TQueryOrCommandType) must implement either IQuery or ICommand interface.");
                    //}
                    QueryOrCommandTypeName = queryOrCommandType?.Name ?? string.Empty;
                }

                public string QueryOrCommandTypeName { get; }
                public string ControllerName { get; init; } = string.Empty;
                public string Action { get; init; } = string.Empty;
                public HttpMethod HttpMethod { get; init; } = HttpMethod.Get;
            }
        }
        """;
}
