namespace ViixsDockerManager.Shared.SourceGenerators.Resolvers;

internal readonly struct ParameterTypeResolveResult
{
    internal ParameterTypeResolveResult(string parameterType, string? returnType = null)
    {
        ParameterType = parameterType;
        ReturnType = returnType;
    }

    public string ParameterType { get; }
    public string? ReturnType { get; }
}
