namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class Modifier
{
    public static readonly Modifier Public = new Modifier("public", 1);
    public static readonly Modifier Internal = new Modifier("internal", 2);
    public static readonly Modifier Private = new Modifier("private", 3);
    public static readonly Modifier Protected = new Modifier("protected", 4);
    public static readonly Modifier Static = new Modifier("static", 5);
    public static readonly Modifier Sealed = new Modifier("sealed", 6);
    public static readonly Modifier Abstract = new Modifier("abstract", 7);

    public int Order { get; }
    public string ModifierString { get; }

    private Modifier(string modifier, int order)
    {
        ModifierString = modifier;
        Order = order;
    }



    public static implicit operator string(Modifier modifier) => modifier.ModifierString;
}
