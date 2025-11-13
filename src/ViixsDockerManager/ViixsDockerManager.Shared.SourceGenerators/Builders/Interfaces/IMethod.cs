using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

/// <summary>
/// Represents a method in a class.
/// </summary>
internal interface IMethod
{
    string GetMethodString(Indentation indentation);
}
