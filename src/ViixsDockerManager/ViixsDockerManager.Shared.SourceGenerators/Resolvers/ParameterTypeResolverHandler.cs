using Microsoft.CodeAnalysis;
using ViixsDockerManager.Shared.SourceGenerators.Helpers;

namespace ViixsDockerManager.Shared.SourceGenerators.Resolvers;

internal static class ParameterTypeResolverHandler
{
    /// <summary>
    /// Resolves the parameter type from the given attribute and named type symbol.
    ///
    /// it first checks if the named type symbol implements a known command or query interface.
    /// if so, it uses the interface's type arguments to resolve the parameter and return types.
    /// else, it checks the constructor of the attribute for concrete type arguments.
    /// </summary>
    /// <param name="attribute">The attribute which has a constructor with a concrete type</param>
    /// <param name="namedTypeSymbol">The interfaces if any</param>
    /// <returns></returns>
    public static ParameterTypeResolveResult? ResolveParameterType(AttributeData attribute, INamedTypeSymbol? namedTypeSymbol = null)
    {
        if (namedTypeSymbol is null)
        {
            return null; //The given type is not implementing any known command or query interface
        }

        var ctorSymbol = attribute.GetConstructorTypeSymbol();

        ParameterTypeResolveResult? result = null;

        if (namedTypeSymbol is not null)
        {
            result = CommandOrQueryInterfaceResolver.Resolve(namedTypeSymbol);
        }

        if (ctorSymbol is not null && result is null)
        {
            result = ConstructorInterfaceResolver.Resolve(ctorSymbol);
        }
        return result;
    }
}
