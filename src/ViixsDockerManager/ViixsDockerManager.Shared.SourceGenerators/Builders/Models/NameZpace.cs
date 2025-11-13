using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class NameZpace(string @namespace) : INameSpace
{
    public string GetNameSpaceString() => $"namespace {@namespace};";
    public static implicit operator NameZpace(string @namespace) => new NameZpace(@namespace);
}
