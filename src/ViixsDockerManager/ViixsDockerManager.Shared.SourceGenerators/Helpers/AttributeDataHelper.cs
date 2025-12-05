using Microsoft.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Helpers;

internal static class AttributeDataHelper
{
    public static ITypeSymbol? GetConstructorTypeSymbol(this AttributeData attributeData, int index = 0)
    {
        if (attributeData.ConstructorArguments.Length == 0)
        {
            return null;
        }

        var typeValue = attributeData.ConstructorArguments[index];

        if (typeValue.Kind != TypedConstantKind.Type)
        {
            return null;
        }

        if (typeValue.Value is not ITypeSymbol typeSymbol)
        {
            return null;
        }
        return typeSymbol;
    }
}
