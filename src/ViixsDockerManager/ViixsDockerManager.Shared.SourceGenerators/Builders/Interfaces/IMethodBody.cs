using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

internal interface IMethodBody
{
    string GetMethodBodyString(Indentation indentation);
}
