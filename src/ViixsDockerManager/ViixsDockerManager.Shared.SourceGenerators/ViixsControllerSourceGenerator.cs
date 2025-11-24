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

    // Diagnostic descriptor for duplicate controller names
    private static readonly DiagnosticDescriptor DuplicateControllerNameDescriptor =
        new DiagnosticDescriptor(
            id: "VDM001",
            title: "Duplicate generated controller name",
            messageFormat: "A controller named '{0}' was already generated. Conflicting handler type: '{1}'.",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

    // Diagnostic descriptor when attribute doesn't supply a concrete command/query type
    private static readonly DiagnosticDescriptor MissingConcreteTypeDescriptor =
        new DiagnosticDescriptor(
            id: "VDM002",
            title: "ViixsController attribute must reference a concrete command or query type",
            messageFormat: "The ViixsController attribute on handler '{0}' must provide a concrete command/query type (e.g. typeof(MyCommand)). The generator could not determine a concrete parameter type.",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

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
            var usedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var allData = allControllers
                .Where(batch => batch is not null)
                .SelectMany(batch => batch!) // flatten batches
                .ToList();

            foreach (var d in allData.Where(x => string.IsNullOrWhiteSpace(x.ParameterType)))
            {
                var location = d.AttributeLocation ?? Location.None;
                var diag = Diagnostic.Create(MissingConcreteTypeDescriptor, location, d.TargetClassName);
                productionContext.ReportDiagnostic(diag);
            }

            // Group by controller name so we generate a single file per controller
            var groups = allData.GroupBy(d => d.GeneratedControllerName, StringComparer.OrdinalIgnoreCase);

            foreach (var group in groups)
            {
                // Pass the grouped controller data to the generator so it can produce a single controller file
                var generatedSourceResult = ViixsControllerGenerator.GenerateControllerSource(group);

                foreach (var (controllerName, source) in generatedSourceResult)
                {
                    if (string.IsNullOrEmpty(controllerName))
                    {
                        continue;
                    }

                    if (usedNames.Add(controllerName))
                    {
                        productionContext.AddSource($"{controllerName}.g.cs", SourceText.From(source, Encoding.UTF8));
                    }
                    else
                    {
                        // Report a diagnostic about duplicate controllerName
                        // Try to include the handler type from the current group for context
                        var conflictingHandler = group.FirstOrDefault().TargetClassName ?? "UnknownHandler";
                        var diag = Diagnostic.Create(DuplicateControllerNameDescriptor, Location.None, controllerName, conflictingHandler);
                        productionContext.ReportDiagnostic(diag);
                    }
                }
            }
        });
    }

    private static IEnumerable<GeneratedControllerData>? GetGeneratedControllerData(GeneratorAttributeSyntaxContext context)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol symbol)
        {
            return [];
        }

        var attributes = symbol.GetAttributes().Where(ad => ad.AttributeClass?.ToDisplayString() == ViixsControllerAttributeDefinition.FullTypeName);
        if (!attributes.Any())
        {
            return [];
        }

        // Try to find a handler-like interface on the symbol
        var commandOrQueryInterface = symbol.AllInterfaces.FirstOrDefault(i =>
        {
            var origininalDefinition = i.OriginalDefinition;
            if (origininalDefinition == null)
            {
                return false;
            }

            if (origininalDefinition.Name == "IQueryHandler" && origininalDefinition.Arity == 2)
            {
                return true;
            }

            return origininalDefinition.Name == "ICommandHandler" && origininalDefinition.Arity == 1;
        });

        var nameSpace = $"{GetNameSpace((BaseTypeDeclarationSyntax)context.TargetNode)}.Controllers"; //TODO: Move to NameZpace ?

        // We'll compute parameter/return per-attribute (so attributes that supply a concrete Type will work even for a generic handler)
        return attributes.Select(attribute =>
        {
            // Try to get a concrete command type from the attribute constructor argument (positional)
            ITypeSymbol? ctorTypeSymbol = null;
            if (attribute.ConstructorArguments.Length > 0 && attribute.ConstructorArguments[0].Kind == TypedConstantKind.Type)
            {
                ctorTypeSymbol = attribute.ConstructorArguments[0].Value as ITypeSymbol;
            }

            string? parameterType = null;
            string? returnType = null;

            // If the discovered interface exists and has concrete type arguments, prefer those.
            if (ctorTypeSymbol is not null)
            {
                parameterType = ctorTypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            }
            else if (commandOrQueryInterface is not null)
            {
                var definitionName = commandOrQueryInterface.OriginalDefinition?.Name;
                // If the interface type arguments are concrete (not type-parameters), use them
                var typeArguments = commandOrQueryInterface.TypeArguments;
                var hasTypeParameters = typeArguments.Any(typeSymbol => typeSymbol.TypeKind == TypeKind.TypeParameter);

                if (!hasTypeParameters)
                {
                    if (definitionName == "IQueryHandler" && typeArguments.Length >= 2)
                    {
                        parameterType = typeArguments[0]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                        returnType = typeArguments[1]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    }
                    else if (definitionName == "ICommandHandler" && typeArguments.Length >= 1)
                    {
                        parameterType = typeArguments[0]?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    }
                }
            }

            // Capture attribute location (if attribute written in source this will be non-null)
            var attributeLocation = attribute.ApplicationSyntaxReference?.GetSyntax() is SyntaxNode attrNode
                ? attrNode.GetLocation()
                : null;

            // Ensure non-null parameterType to satisfy GeneratedControllerData
            parameterType ??= string.Empty;

            // Read named arguments safely
            var controllerNameKv = attribute.NamedArguments.FirstOrDefault(na => na.Key == "ControllerName");
            var actionKv = attribute.NamedArguments.FirstOrDefault(na => na.Key == "Action");
            var httpMethodKv = attribute.NamedArguments.FirstOrDefault(na => na.Key == "HttpMethod");

            var controllerNameValue = controllerNameKv.Value.Value as string ?? string.Empty; //TODO: report diagnostic, controller value is required
            var actionValue = actionKv.Value.Value as string ?? string.Empty;
            var httpMethodString = httpMethodKv.Value.Value as string ?? string.Empty;

            var newClassName = controllerNameValue;
            if (string.IsNullOrWhiteSpace(newClassName))
            {
                const string handler = "Handler";
                if (!symbol.Name.EndsWith(handler))
                {
                    //TODO: Report diagnostic
                    throw new Exception($"Class '{symbol.Name}' must end with '{handler}' or specify a ControllerName in the ViixController attribute.");
                }

                newClassName = symbol.Name.Substring(0, symbol.Name.Length - handler.Length) + "Controller";
            }

            if (string.IsNullOrWhiteSpace(httpMethodString))
            {
                httpMethodString = HttpMethod.Get.Method;
            }

            return new GeneratedControllerData(
                actionValue,
                symbol.Name,
                symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                newClassName,
                nameSpace,
                httpMethodString,
                parameterType,
                returnType,
                attributeLocation);
        });
    }

    // Pseudocode / Plan:
    // 1. Find the nearest namespace parent for the provided syntax node.
    // 2. Walk up any nested namespace declarations collecting each namespace "Name" string.
    //    - For constructs like `namespace a.b.c { }` the `Name.ToString()` will already contain dots.
    //    - For nested declarations like `namespace a { namespace b { } }` we'll collect "b" and "a" separately.
    // 3. Reverse the collected parts so they are ordered outermost -> innermost.
    // 4. Join the parts with '.' to produce a full namespace string.
    // 5. Split that full namespace on '.' to get all segments.
    // 6. Return the first two segments joined with '.' (if available), otherwise return whatever segments exist.
    // 7. If no namespace is found, return an empty string.
    //
    // This ensures that for "viixsdockermanager.version.Handlers" we return "viixsdockermanager.version"
    // and also handles file-scoped namespaces and nested namespace declarations.

    private static string GetNameSpace(BaseTypeDeclarationSyntax syntax)
    {
        const string dot = ".";
        const char dotChar = '.';
        var potentialNamespaceParent = syntax.Parent;

        // Find the nearest namespace (namespace declaration or file-scoped namespace)
        while (potentialNamespaceParent != null
               && potentialNamespaceParent is not NamespaceDeclarationSyntax
               && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        if (potentialNamespaceParent is not BaseNamespaceDeclarationSyntax namespaceParent)
        {
            return string.Empty;
        }

        // Collect namespace name parts from inner -> outer
        List<string> parts = [];
        var current = namespaceParent;
        while (current != null)
        {
            // current.Name.ToString() may contain dots if the declaration used a qualified name like "a.b.c"
            parts.Add(current.Name.ToString());

            if (current.Parent is BaseNamespaceDeclarationSyntax parentNamespace)
            {
                current = parentNamespace;
            }
            else
            {
                break;
            }
        }

        // Now we have inner -> outer, reverse to get outer -> inner
        parts.Reverse();

        // Build full namespace string (handles both dotted single declarations and nested declarations)
        var fullNamespace = string.Join(dot, parts.Where(p => !string.IsNullOrWhiteSpace(p)));

        if (string.IsNullOrWhiteSpace(fullNamespace))
        {
            return string.Empty;
        }

        // Split into segments and take the first two segments (or fewer if not available)
        var segments = fullNamespace.Split([dotChar], StringSplitOptions.RemoveEmptyEntries);
        var takeCount = Math.Min(2, segments.Length);

        return string.Join(dot, segments.Take(takeCount));
    }
}
