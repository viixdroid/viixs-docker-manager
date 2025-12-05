using Microsoft.CodeAnalysis;

namespace ViixsDockerManager.Shared.SourceGenerators.Helpers;

internal static class NamedTypeSymbolHelper
{
    private const string IQueryHandlerName = "IQueryHandler";
    private const string ICommandHandlerName = "ICommandHandler";
    public static INamedTypeSymbol? GetCommandOrQueryInterface(this INamedTypeSymbol namedTypeSymbol)
    {
        return namedTypeSymbol.AllInterfaces.FirstOrDefault(i =>
        {
            var origininalDefinition = i.OriginalDefinition;
            if (origininalDefinition == null)
            {
                return false;
            }

            if (origininalDefinition.Name == IQueryHandlerName && origininalDefinition.Arity == 2)
            {
                return true;
            }

            return origininalDefinition.Name == ICommandHandlerName && origininalDefinition.Arity == 1;
        });
    }
}
