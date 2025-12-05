using Microsoft.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Resolvers;

internal interface IParameterTypeResolver
{
    /// <summary>
    /// Resolves the return type and paramter type when needed
    /// </summary>
    /// <param name="attribute">If the namedtypesymbol is null or contains a generic typeparameter, we will check if there is constructor argument which must have a concrete type</param>
    /// <param name="namedTypeSymbol">Used when the typeparameters are concrete types</param>
    /// <returns>
    /// A <see cref="ParameterTypeResolveResult"/> object with filled parametertype and returntype in case of query interface
    /// A <see cref="ParameterTypeResolveResult"/> object with filled parametertype in case of command interface
    /// null when no result is passed.
    /// </returns>
    ParameterTypeResolveResult? Resolve(AttributeData attribute, INamedTypeSymbol? namedTypeSymbol = null);
}
