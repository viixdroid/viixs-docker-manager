using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ViixDockerManager.Shared.SourceGenerators.Constants;
using ViixDockerManager.Shared.SourceGenerators.Models;

namespace ViixDockerManager.Shared.SourceGenerators;

[Generator]
public class ViixControllerSourceGenerator : IIncrementalGenerator
{
    private const string IQueryUnboundName = "ViixsDockerManager.Mediator.Queries.IQueryHandler<,>";
    private const string ICommandUnboundName = "ViixsDockerManager.Mediator.Commands.ICommandHandler<>";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(i =>
        {
            i.AddEmbeddedAttributeDefinition();
            i.AddSource(ViixControllerAttributeDefinition.FileName, SourceText.From(ViixControllerAttributeDefinition.ViixControllerAttributeText, Encoding.UTF8));
        });

        IncrementalValuesProvider<IEnumerable<GeneratedControllerData>?> controllersToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "NetEscapades.EnumGenerators.EnumExtensionsAttribute",
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: static (context, _) => GetGeneratedControllerData(context))
            .Where(static m => m is not null);

        IncrementalValueProvider<ImmutableArray<IEnumerable<GeneratedControllerData>?>> allControllersToGenerate = controllersToGenerate.Collect();

        context.RegisterSourceOutput(allControllersToGenerate, (spc, allControllers) =>
        {
            foreach (var controllers in allControllers)
            {
                if (controllers is null)
                {
                    continue;
                }
                //TODO: Group by class name to avoid duplicates
                //TODO: actually generate classes.

                foreach (var controllerData in controllers)
                {
                    var source = ViixControllerGenerator.GenerateControllerSource(controllerData);
                    spc.AddSource($"{controllerData.GeneratedClassName}Controller.g.cs", SourceText.From(source, Encoding.UTF8));
                }
            }
        });
    }

    private static IEnumerable<GeneratedControllerData>? GetGeneratedControllerData(GeneratorAttributeSyntaxContext context)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol symbol || symbol.IsGenericType)
        {
            return [];
        }

        var attributes = symbol.GetAttributes().Where(ad => ad.AttributeClass?.ToDisplayString() == ViixControllerAttributeDefinition.FullTypeName);
        if (!attributes.Any())
        {
            return [];
        }

        var commandOrQueryInterface = symbol.AllInterfaces.FirstOrDefault
            (i => i.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == IQueryUnboundName
                            || i.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == ICommandUnboundName
            );

        if (commandOrQueryInterface is null)
        {
            return [];
        }


        var unboundName = commandOrQueryInterface.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        string? parameterType = null!;
        string? returnType = null!;

        // Determine the parameter and return type based on which interface was implemented
        if (unboundName == IQueryUnboundName)
        {
            parameterType = commandOrQueryInterface.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat); // TJobData
            returnType = commandOrQueryInterface.TypeArguments[1].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat); // TResult
        }
        else if (unboundName == ICommandUnboundName)
        {
            parameterType = commandOrQueryInterface.TypeArguments[0].ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat); // TInput
        }

        return attributes.Select(attribute =>
        {
            var controllerName = attribute.NamedArguments.FirstOrDefault(na => na.Key == "ControllerName");
            var action = attribute.NamedArguments.FirstOrDefault(na => na.Key == "Action");
            var httpMethod = attribute.NamedArguments.FirstOrDefault(na => na.Key == "HttpMethod");

            string newClassName = attribute.ConstructorArguments[0].Value?.ToString() ?? "DefaultClass";
            string methodName = attribute.ConstructorArguments[1].Value?.ToString() ?? "DefaultMethod";

            return new GeneratedControllerData(
                action.Value.ToString(),
                symbol.Name,
                symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                newClassName,
                methodName,
                parameterType,
                returnType);
        });
    }
}
