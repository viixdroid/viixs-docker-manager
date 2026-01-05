using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using ViixsDockerManager.Shared.SourceGenerators.Constants;
using ViixsDockerManager.Shared.SourceGenerators.Helpers;
using ViixsDockerManager.Shared.SourceGenerators.Models;
using ViixsDockerManager.Shared.SourceGenerators.Resolvers;

namespace ViixsDockerManager.Shared.SourceGenerators;

[Generator]
public class ViixsControllerSourceGenerator : IIncrementalGenerator
{
    private const string IQueryHandlerName = "IQueryHandler";
    private const string ICommandHandlerName = "ICommandHandler";

    //// Diagnostic descriptor for duplicate controller names
    //private static readonly DiagnosticDescriptor DuplicateControllerNameDescriptor =
    //    new DiagnosticDescriptor(
    //        id: "VDM001",
    //        title: "Duplicate generated controller name",
    //        messageFormat: "A controller namedTypeSymbol '{0}' was already generated. Conflicting handler typeSymbol: '{1}'.",
    //        category: "ViixsSourceGenerator",
    //        defaultSeverity: DiagnosticSeverity.Warning,
    //        isEnabledByDefault: true
    //    );

    //// Diagnostic descriptor when attribute doesn't supply a concrete command/query typeSymbol
    //private static readonly DiagnosticDescriptor MissingConcreteTypeDescriptor =
    //    new DiagnosticDescriptor(
    //        id: "VDM002",
    //        title: "ViixsController attribute must reference a concrete command or query typeSymbol",
    //        messageFormat: "The ViixsController attribute on handler '{0}' must provide a concrete command/query typeSymbol (e.g. typeof(MyCommand)). The generator cannot know at compile time what the concrete type is.",
    //        category: "ViixsSourceGenerator",
    //        defaultSeverity: DiagnosticSeverity.Error,
    //        isEnabledByDefault: true
    //    );

    //// Diagnostic when no controller name can be inferred and none supplied
    //private static readonly DiagnosticDescriptor MissingControllerNameDescriptor =
    //    new DiagnosticDescriptor(
    //        id: "VDM003",
    //        title: "ControllerName could not be inferred",
    //        messageFormat: "The ViixsController attribute on handler '{0}' did not supply a ControllerName and a default could not be inferred. Specify ControllerName in the attribute.",
    //        category: "ViixsSourceGenerator",
    //        defaultSeverity: DiagnosticSeverity.Error,
    //        isEnabledByDefault: true
    //    );

    //// Diagnostic when no controller name can be inferred and none supplied
    //private static readonly DiagnosticDescriptor MissingInterfacesDescriptor =
    //    new DiagnosticDescriptor(
    //        id: "VDM004",
    //        title: "No ICommandHandler or IQueryHandler interface",
    //        messageFormat: "The class '{0}' does not implement any ICommandHandler or IQueryHandler interfaces and cannot generate controllers",
    //        category: "ViixsSourceGenerator",
    //        defaultSeverity: DiagnosticSeverity.Error,
    //        isEnabledByDefault: true
    //    );

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(i =>
        {
            i.AddEmbeddedAttributeDefinition();
            i.AddSource(ViixsControllerAttributeDefinition.FileName, SourceText.From(ViixsControllerAttributeDefinition.ViixControllerAttributeText, Encoding.UTF8));
        });

        var controllersToGenerate = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                ViixsControllerAttributeDefinition.FullTypeName,
                predicate: static (node, _) => node is ClassDeclarationSyntax,
                transform: static (context, _) => GetGeneratedControllerData(context))
            .Where(static generatedControllerData => generatedControllerData is not null);

        // Flatten the per-class enumerable into a stream of individual GeneratedControllerData items
        var flattenedControllers = controllersToGenerate
            .SelectMany(static (batch, _) => batch ?? []);

        var allControllersToGenerate = flattenedControllers.Collect();

        context.RegisterSourceOutput(allControllersToGenerate, (sourceProductionContext, allControllers) =>
        {
            var usedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var generatedControllerDataList = allControllers.ToList();

            // Report missing concrete type diagnostics
            CheckGeneratedControllerData(sourceProductionContext, generatedControllerDataList);

            // Group by controller name using an explicit dictionary to avoid LINQ allocations
            var groups = new Dictionary<string, List<GeneratedControllerData>>(StringComparer.OrdinalIgnoreCase);
            foreach (var generatedControllerData in generatedControllerDataList)
            {
                var key = generatedControllerData.GeneratedControllerName ?? string.Empty;
                if (!groups.TryGetValue(key, out var list))
                {
                    list = [];
                    groups[key] = list;
                }
                list.Add(generatedControllerData);
            }

            foreach (var kv in groups)
            {
                var controllerNameKey = kv.Key;
                if (string.IsNullOrEmpty(controllerNameKey))
                {
                    // Skip generation for items where controller name couldn't be inferred; diagnostic already reported
                    continue;
                }

                var group = kv.Value;

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
                        sourceProductionContext.AddSource($"{controllerName}.g.cs", SourceText.From(source, Encoding.UTF8));
                    }
                    else
                    {
                        // Report a diagnostic about duplicate controllerName
                        // Try to include the handler typeSymbol from the current group for context
                        var conflictingHandler = group.Count > 0 ? group[0].TargetClassName : "UnknownHandler";
                        DiagnosticDescriptorReporter.DuplicateControllerName.ReportDiagnostic(sourceProductionContext, group.Count > 0 ? group[0] : null, controllerName, conflictingHandler);
                    }
                }
            }
        });
    }

    private static void CheckGeneratedControllerData(SourceProductionContext sourceProductionContext, List<GeneratedControllerData> generatedControllerDataList)
    {
        foreach (var generatedControllerData in generatedControllerDataList)
        {
            if (string.IsNullOrWhiteSpace(generatedControllerData.ParameterType))
            {
                DiagnosticDescriptorReporter.MissingConcreteType.ReportDiagnostic(sourceProductionContext, generatedControllerData);
            }

            // Report missing controller name (inference failed and attribute didn't supply one)
            if (string.IsNullOrWhiteSpace(generatedControllerData.GeneratedControllerName))
            {
                DiagnosticDescriptorReporter.MissingControllerName.ReportDiagnostic(sourceProductionContext, generatedControllerData);
            }

            if (generatedControllerData.Action == "MissingInterfacesDescriptor")
            {
                DiagnosticDescriptorReporter.MissingInterfaces.ReportDiagnostic(sourceProductionContext, generatedControllerData);
            }
        }
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

        var commandOrQueryInterface = symbol.GetCommandOrQueryInterface();

        var nameSpace = $"{GetNameSpace((BaseTypeDeclarationSyntax)context.TargetNode)}.Controllers"; //TODO: Move to NameZpace ?

        // We'll compute parameter/return per-attribute (so attributes that supply a concrete Type will work even for a generic handler)
        return attributes.Select(attribute =>
        {
            // Capture attribute location (if attribute written in source this will be non-null)
            var attributeLocation = attribute.ApplicationSyntaxReference?.GetSyntax() is SyntaxNode attrNode
                ? attrNode.GetLocation()
                : null;

            if (commandOrQueryInterface is null)
            {
                return new GeneratedControllerData(
                    "MissingInterfacesDescriptor",
                    symbol.Name,
                    string.Empty,
                    "MissingInterfacesDescriptor",
                    string.Empty,
                    string.Empty,
                    "MissingInterfacesDescriptor",
                    null,
                    attributeLocation);
            }
            var parameterTypeResolveResult = ParameterTypeResolverHandler.ResolveParameterType(attribute, commandOrQueryInterface);

            // Read namedTypeSymbol arguments safely
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
                    // Cannot infer name; return an entry with empty GeneratedControllerName so the caller can report a diagnostic instead of throwing
                    return new GeneratedControllerData(
                        actionValue,
                        symbol.Name,
                        string.Empty,
                        string.Empty,
                        nameSpace,
                        httpMethodString,
                        parameterTypeResolveResult?.ParameterType ?? string.Empty,
                        parameterTypeResolveResult?.ReturnType,
                        attributeLocation);
                }

                newClassName = symbol.Name.Substring(0, symbol.Name.Length - handler.Length) + "Controller";
            }

            if (string.IsNullOrWhiteSpace(httpMethodString))
            {
                httpMethodString = HttpMethod.Get.Method;
            }

            // Avoid calling ToDisplayString here (can be expensive); leave FullTargetTypeName empty unless needed later
            return new GeneratedControllerData(
                actionValue,
                symbol.Name,
                string.Empty,
                newClassName,
                nameSpace,
                httpMethodString,
                parameterTypeResolveResult?.ParameterType ?? string.Empty,
                parameterTypeResolveResult?.ReturnType,
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
