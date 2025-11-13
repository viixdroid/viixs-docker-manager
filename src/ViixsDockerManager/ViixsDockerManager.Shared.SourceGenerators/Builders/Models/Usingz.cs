using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class Usingz(string @using) : IUsing
{
    public string GetUsingString() => $"using {@using};";

    public static implicit operator Usingz(string @using) => new Usingz(@using);
}
