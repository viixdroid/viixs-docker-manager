using System.Text;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Interfaces;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders;

internal class ClassBuilder : BuilderBase<Classz>
{
    private ClassBuilder()
        : base()
    {
    }

    internal static ClassBuilder Create()
    {
        return new ClassBuilder();
    }

    internal ClassBuilder WithClassName(string className)
    {
        ToBuild.ClassName = className;
        return this;
    }

    internal ClassBuilder WithNameSpace(NameZpace nameSpace)
    {
        ToBuild.NameSpace = nameSpace;
        return this;
    }

    internal ClassBuilder WithModifier(Modifier modifier)
    {
        ToBuild.Modifiers?.Add(modifier);
        return this;
    }

    internal ClassBuilder WithMethod(IMethod method)
    {
        ToBuild.Methods?.Add(method);
        return this;
    }

    internal ClassBuilder WithMethod(Action<MethodBuilder> methodBuilderAction)
    {
        var methodBuilder = MethodBuilder.Create();
        methodBuilderAction(methodBuilder);
        ToBuild.Methods?.Add(methodBuilder.Build());
        return this;
    }

    internal ClassBuilder WithUsing(Usingz @using)
    {
        ToBuild.Usings?.Add(@using);
        return this;
    }
}
