using Microsoft.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Models;

public readonly record struct GeneratedControllerData
{
    public readonly string Action;
    public readonly string TargetClassName;
    public readonly string FullTargetTypeName;
    public readonly string GeneratedControllerName;
    public readonly string GeneratedHttpMethodName;
    public readonly string GeneratedNamespace;
    public readonly string ParameterType;
    public readonly string? ReturnType;
    public readonly Location? AttributeLocation;

    public GeneratedControllerData(
        string action,
        string targetClassName,
        string fullTargetTypeName,
        string generatedControllerName,
        string generatedNamespace,
        string generatedHttpMethodName,
        string parameterType,
        string? returnType,
        Location? attributeLocation = null)
    {
        Action = action;
        TargetClassName = targetClassName;
        FullTargetTypeName = fullTargetTypeName;
        GeneratedControllerName = generatedControllerName;
        GeneratedNamespace = generatedNamespace;
        GeneratedHttpMethodName = generatedHttpMethodName;
        ParameterType = parameterType;
        ReturnType = returnType;
        AttributeLocation = attributeLocation;
    }

}
