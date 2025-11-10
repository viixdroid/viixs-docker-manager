using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ViixsDockerManager.Shared.SourceGenerators.Constants;
using ViixsDockerManager.Shared.SourceGenerators.Models;

namespace ViixsDockerManager.Shared.SourceGenerators;

[Generator]
public class ViixsControllerSourceGenerator : IIncrementalGenerator
{
    private const string IQueryUnboundName = "ViixsDockerManager.Mediator.Queries.IQueryHandler<,>";
    private const string ICommandUnboundName = "ViixsDockerManager.Mediator.Commands.ICommandHandler<>";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(i =>
        {
            i.AddEmbeddedAttributeDefinition();
            i.AddSource(ViixsControllerAttributeDefinition.FileName, SourceText.From(ViixsControllerAttributeDefinition.ViixControllerAttributeText, Encoding.UTF8));
        });

        IncrementalValuesProvider<IEnumerable<GeneratedControllerData>?> controllersToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ViixsControllerAttributeDefinition.FullTypeName,
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: static (context, _) => GetGeneratedControllerData(context))
            .Where(static generatedControllerData => generatedControllerData is not null);

        IncrementalValueProvider<ImmutableArray<IEnumerable<GeneratedControllerData>?>> allControllersToGenerate = controllersToGenerate.Collect();

        context.RegisterSourceOutput(allControllersToGenerate, (productionContext, allControllers) =>
        {
            foreach (var controllers in allControllers)
            {
                if (controllers is null)
                {
                    continue;
                }
                //TODO: Group by class name to avoid duplicates
                //TODO: actually generate classes.

                var generatedSourceResult = ViixsControllerGenerator.GenerateControllerSource(controllers);

                foreach (var (controllerName, source) in generatedSourceResult)
                {
                    if (string.IsNullOrEmpty(controllerName))
                    {
                        continue;
                    }
                    //foreach (var controllerData in controllers)
                    //{
                    productionContext.AddSource($"{controllerName}.g.cs", SourceText.From(source, Encoding.UTF8));
                }
                //}
            }
        });
    }

    private static IEnumerable<GeneratedControllerData>? GetGeneratedControllerData(GeneratorAttributeSyntaxContext context)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol symbol || symbol.IsGenericType)
        {
            return [];
        }

        var attributes = symbol.GetAttributes().Where(ad => ad.AttributeClass?.ToDisplayString() == ViixsControllerAttributeDefinition.FullTypeName);
        if (!attributes.Any())
        {
            return [];
        }

        //var commandOrQueryInterface = symbol.AllInterfaces.FirstOrDefault
        //    (i => i.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == IQueryUnboundName
        //                    || i.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) == ICommandUnboundName
        //    );

        var commandOrQueryInterface = symbol.AllInterfaces.FirstOrDefault(i =>
            // match IQueryHandler<TQuery, TResult>
            (i.OriginalDefinition.Name == "IQueryHandler" && i.OriginalDefinition.Arity == 2)
            // OR match ICommandHandler<TInput>
            || (i.OriginalDefinition.Name == "ICommandHandler" && i.OriginalDefinition.Arity == 1)
        );

        if (commandOrQueryInterface is null)
        {
            return [];
        }

        var nameSpace = $"{GetNameSpace((BaseTypeDeclarationSyntax)context.TargetNode)}.Controllers";

        var unboundName = commandOrQueryInterface.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        string? parameterType = null!;
        string? returnType = null!;

        // Determine the parameter and return type based on which interface was implemented
        if (commandOrQueryInterface.Name == "IQueryHandler")
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
            var controllerName = attribute.NamedArguments.FirstOrDefault(na => na.Key == "ControllerName").Value;
            var action = attribute.NamedArguments.FirstOrDefault(na => na.Key == "Action").Value;
            var httpMethod = attribute.NamedArguments.FirstOrDefault(na => na.Key == "HttpMethod").Value;

            var newClassName = controllerName.Value as string;
            if (string.IsNullOrWhiteSpace(newClassName))
            {
                const string handler = "Handler";
                if (!symbol.Name.EndsWith(handler))
                {
                    throw new Exception($"Class '{symbol.Name}' must end with '{handler}' or specify a ControllerName in the ViixController attribute.");
                }

                var handlerLength = handler.Length;
                var controllerNameFromHandler = symbol.Name.Substring(0, symbol.Name.Length - handlerLength);
                newClassName = $"{controllerNameFromHandler}Controller";
            }
            //string methodName = attribute.ConstructorArguments[1].Value?.ToString() ?? "DefaultMethod";

            var httpMethodString = httpMethod.Value as string;
            if(string.IsNullOrWhiteSpace(httpMethodString))
            {
                httpMethodString = HttpMethod.Get.Method;
            }

            return new GeneratedControllerData(
                action.Value as string ?? "",
                symbol.Name,
                symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                newClassName!,
                nameSpace,
                httpMethodString!,
                parameterType,
                returnType);
        });
    }

    private static string GetNameSpace(BaseTypeDeclarationSyntax syntax)
    {
        var nameSpace = string.Empty;

        var potentialNamespaceParent = syntax.Parent;

        while (potentialNamespaceParent != null
               && potentialNamespaceParent is not NamespaceDeclarationSyntax
               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
        {
            nameSpace = namespaceParent.Name.ToString();

            while (true)
            {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                {
                    break;
                }

                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
                namespaceParent = parent;
            }
        }
        return nameSpace;
    }
}
