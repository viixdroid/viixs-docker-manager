using System.Text;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

internal class MethodBody : IMethodBody
{
    public Queue<string> Lines { get; }

    public MethodBody()
    {
        Lines = [];
    }

    public string GetMethodBodyString(Indentation indentation)
    {
        var stringBuilder = new StringBuilder();
        foreach (var line in Lines)
        {
            stringBuilder.AppendLine($"{Indentation.Indent4}{indentation}{line}");
        }

        return stringBuilder.ToString().TrimEnd();
        //return string.Join($"\r\n{Indentation.Indent4}{indentation}", Lines);
    }
}
