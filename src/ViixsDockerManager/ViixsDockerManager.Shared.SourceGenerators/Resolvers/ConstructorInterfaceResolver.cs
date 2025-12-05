using Microsoft.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Resolvers;

internal static class ConstructorInterfaceResolver
{
    public static ParameterTypeResolveResult? Resolve(ITypeSymbol constructorSymbol)
    {        
        var parameterType = constructorSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        return new ParameterTypeResolveResult(parameterType);
    }
}
