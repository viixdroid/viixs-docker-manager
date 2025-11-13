using System.Text;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;
using static ViixsDockerManager.Shared.SourceGenerators.Constants.ClassSymbolsConstants;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class Method : IMethod
{
    public ICollection<Modifier>? Modifiers { get; }
    public string? Name { get; set; }
    public string? ReturnType { get; set; }
    public IMethodBody? Body { get; set; }
    public ICollection<IParameter>? Parameters { get; }

    public Method()
    {
        Modifiers = [];
        Parameters = [];
    }

    public string GetMethodString(Indentation indentation)
    {
        var paramsString = string.Join(", ", Parameters.Select(p => p.GetParameterString()));
        var modifiersString = string.Join(" ", Modifiers.OrderBy(m => m.Order).Select(modifier => (string)modifier));

        var signature = $"{indentation}{modifiersString} {ReturnType} {Name}({string.Join(", ", paramsString)})";
        var stringBuilder = new StringBuilder(signature);
        stringBuilder.AppendLine();
        stringBuilder.AppendLine($"{indentation}{OpenBrace}");
        stringBuilder.AppendLine($"{Body?.GetMethodBodyString(indentation)}");
        stringBuilder.AppendLine($"{indentation}{CloseBrace}");
        return stringBuilder.ToString();
    }
}
