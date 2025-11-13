namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class Indentation
{
    public static Indentation None { get; } = new Indentation(string.Empty);
    public static Indentation Indent2 { get; } = new Indentation("  ");
    public static Indentation Indent4 { get; } = new Indentation("    ");
    public static Indentation Indent8 { get; } = new Indentation("        ");

    public string IndentString { get; }
    private Indentation(string indentString)
    {
        IndentString = indentString;
    }

    public override string ToString() => IndentString;
}
