using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class Parameter(string parameterName, string parameterType, bool isExtension = false) : IParameter
{
    public string GetParameterString() => isExtension ? $"this {parameterType} {parameterName}" : $"{parameterType} {parameterName}";
}
