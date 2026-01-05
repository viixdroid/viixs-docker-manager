using Microsoft.CodeAnalysis;

using static ViixsDockerManager.Shared.SourceGenerators.Constants.TypeNamesConstants;

namespace ViixsDockerManager.Shared.SourceGenerators.Resolvers;

internal static class CommandOrQueryInterfaceResolver
{
    public static ParameterTypeResolveResult? Resolve(INamedTypeSymbol? namedTypeSymbol = null)
    {
        if (namedTypeSymbol is null)
        {
            return null;
        }

        var definitionName = namedTypeSymbol.OriginalDefinition?.Name;
        var typeArguments = namedTypeSymbol.TypeArguments;

        var hasConcreteTypeArgument = typeArguments.Any(IsConcreteType);

        string? parameterType = null;
        string? returnType = null;
        if (!hasConcreteTypeArgument)
        {
            return null;
        }

        if (definitionName == IQueryHandlerName && typeArguments.Length >= 2)
        {
            parameterType = typeArguments[0]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            returnType = typeArguments[1]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
        else if (definitionName == ICommandHandlerName && typeArguments.Length >= 1)
        {
            parameterType = typeArguments[0]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        if (parameterType is null || parameterType == string.Empty)
        {
            return null;
        }

        return new ParameterTypeResolveResult(parameterType, returnType);
    }

    private static bool IsConcreteType(ITypeSymbol? typeSymbol)
    {
        if (typeSymbol is null)
        {
            return false;
        }

        // Type parameters and error types are not concrete.
        if (typeSymbol.TypeKind == TypeKind.TypeParameter || typeSymbol.TypeKind == TypeKind.Error)
        {
            return false;
        }

        // Arrays / pointers: check element / pointed types.
        if (typeSymbol is IArrayTypeSymbol arrayType)
        {
            return IsConcreteType(arrayType.ElementType);
        }

        if (typeSymbol is IPointerTypeSymbol pointerType)
        {
            return IsConcreteType(pointerType.PointedAtType);
        }

        // Named types: unbound generic (e.g. List<>) is not concrete.
        if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
        {
            if (namedTypeSymbol.IsUnboundGenericType)
            {
                return false;
            }

            // Ensure every typeSymbol argument is itself concrete (recursive).
            foreach (var namedTypeSymbolTypeArgument in namedTypeSymbol.TypeArguments)
            {
                if (!IsConcreteType(namedTypeSymbolTypeArgument))
                {
                    return false;
                }
            }
        }

        // primitives and non-generic declared types are concrete
        return true;
    }
}
