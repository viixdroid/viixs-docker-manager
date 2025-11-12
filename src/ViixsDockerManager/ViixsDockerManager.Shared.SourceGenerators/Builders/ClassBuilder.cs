using System.Text;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders;

internal class ClassBuilder
{
    private readonly string _className;
    private readonly ICollection<Modifier> _modifiers = [];
    private readonly ICollection<IMethod> _methods = [];
    private readonly ICollection<IUsing> _usings = [];

    private ClassBuilder(string className)
    {
        _className = className;
    }

    internal static ClassBuilder Create(string className)
    {
        return new ClassBuilder(className);
    }

    internal ClassBuilder WithModifier(Modifier modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    internal ClassBuilder WithMethod(IMethod method)
    {
        _methods.Add(method);
        return this;
    }

    internal ClassBuilder WithUsings(IUsing @using)
    {
        _usings.Add(@using);
        return this;
    }

    internal IClass Build()
    {
        var stringBuilder = new StringBuilder();
        foreach (var usingString in _usings)
        {
            stringBuilder.AppendLine(usingString.GetUsingString());
        }
        if (_usings.Count > 0)
        {
            stringBuilder.AppendLine();
        }
        var modifiersString = string.Join(" ", _modifiers.OrderBy(m => m.Order).Select(modifier => (string)modifier));
        stringBuilder.AppendLine($"{modifiersString} class {_className}");
        stringBuilder.AppendLine("{");
        foreach (var method in _methods)
        {
            var methodString = method.GetMethodString();
            var indentedMethodString = IndentMethodString(methodString);
            stringBuilder.AppendLine(indentedMethodString);
        }
        stringBuilder.AppendLine("}");
        return stringBuilder.ToString();
    }   
}
