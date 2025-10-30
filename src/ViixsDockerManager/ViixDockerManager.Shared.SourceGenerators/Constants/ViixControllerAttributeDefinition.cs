using System.Diagnostics.CodeAnalysis;

namespace ViixDockerManager.Shared.SourceGenerators.Constants;

internal static class ViixControllerAttributeDefinition
{
    
    public const string FileName = "ViixControllerAttribute.g.cs";
    public const string FullTypeName = "ViixDockerManager.Shared.Attributes.ViixControllerAttribute`1";
    //TODO: Move to file and read from there. Or embed as resource.
    public const string ViixControllerAttributeText = """
        using System;
        namespace ViixDockerManager.Shared.Attributes;
        
        [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
        [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
        public sealed class ViixControllerAttribute<TQueryOrCommandType> : Attribute
            where TQueryOrCommandType : class
        {
            public ViixControllerAttribute()
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
        """;
}
