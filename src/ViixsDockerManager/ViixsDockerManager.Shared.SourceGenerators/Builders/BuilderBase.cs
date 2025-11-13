namespace ViixsDockerManager.Shared.SourceGenerators.Builders;

internal abstract class BuilderBase<TToBuild>
    where TToBuild : class, new()
{
    protected TToBuild ToBuild { get; }

    protected BuilderBase()
    {
        ToBuild = new TToBuild();
    }

    public virtual TToBuild Build()
    {
        return ToBuild;
    }
}
