using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Builders;

internal class MethodBuilder : BuilderBase<Method>
{
    private MethodBuilder()
        : base()
    {
    }

    internal static MethodBuilder Create()
    {
        return new MethodBuilder();
    }
    internal MethodBuilder WithName(string name)
    {
        ToBuild.Name = name;
        return this;
    }
    internal MethodBuilder WithReturnType(string returnType)
    {
        ToBuild.ReturnType = returnType;
        return this;
    }

    internal MethodBuilder WithBody(Action<MethodBodyBuilder> bodyBuilderAction)
    {
        var bodyBuilder = MethodBodyBuilder.Create();
        bodyBuilderAction(bodyBuilder);
        ToBuild.Body = bodyBuilder.Build();
        return this;
    }

    internal MethodBuilder WithModifier(Modifier modifier)
    {
        ToBuild.Modifiers?.Add(modifier);
        return this;
    }
    internal MethodBuilder WithParameter(string parameterName, string parameterType, bool isExtension = false)
    {
        ToBuild.Parameters?.Add(new Parameter(parameterName, parameterType, isExtension));
        return this;
    }
}
