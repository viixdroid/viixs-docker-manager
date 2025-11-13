using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders;

internal class MethodBodyBuilder : BuilderBase<MethodBody>
{
    private MethodBodyBuilder()
    {
    }

    public static MethodBodyBuilder Create()
    {
        return new MethodBodyBuilder();
    }

    public MethodBodyBuilder AddLine()
    {
        ToBuild.Lines.Enqueue(string.Empty);
        return this;
    }

    public MethodBodyBuilder AddLine(string line)
    {
        ToBuild.Lines.Enqueue(line);
        return this;
    }
}
