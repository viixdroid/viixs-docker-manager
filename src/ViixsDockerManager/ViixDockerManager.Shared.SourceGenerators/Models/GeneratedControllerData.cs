namespace ViixDockerManager.Shared.SourceGenerators.Models;

public readonly record struct GeneratedControllerData
{
    public readonly string Action;
    public readonly string TargetClassName;
    public readonly string FullTargetTypeName;
    public readonly string GeneratedClassName;
    public readonly string GeneratedMethodName;
    public readonly string ParameterType;
    public readonly string? ReturnType;

    public GeneratedControllerData(string action, string targetClassName, string fullTargetTypeName, string generatedClassName, string generatedMethodName, string parameterType, string? returnType)
    {
        Action = action;
        TargetClassName = targetClassName;
        FullTargetTypeName = fullTargetTypeName;
        GeneratedClassName = generatedClassName;
        GeneratedMethodName = generatedMethodName;
        ParameterType = parameterType;
        ReturnType = returnType;
    }

}
