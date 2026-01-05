using Microsoft.CodeAnalysis;
using ViixsDockerManager.Shared.SourceGenerators.Models;

namespace ViixsDockerManager.Shared.SourceGenerators.Helpers;

internal static class DiagnosticDescriptorHelper
{
    internal static readonly DiagnosticDescriptor DuplicateControllerName =
        new DiagnosticDescriptor(
            id: "VDM001",
            title: "Duplicate generated controller name",
            messageFormat: "A controller namedTypeSymbol '{0}' was already generated. Conflicting handler typeSymbol: '{1}'.",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true
        );

    internal static readonly DiagnosticDescriptor MissingConcreteType =
        new DiagnosticDescriptor(
            id: "VDM002",
            title: "ViixsController attribute must reference a concrete command or query typeSymbol",
            messageFormat: "The ViixsController attribute on handler '{0}' must provide a concrete command/query typeSymbol (e.g. typeof(MyCommand)). The generator cannot know at compile time what the concrete type is.",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static readonly DiagnosticDescriptor MissingControllerName =
        new DiagnosticDescriptor(
            id: "VDM003",
            title: "ControllerName could not be inferred",
            messageFormat: "The ViixsController attribute on handler '{0}' did not supply a ControllerName and a default could not be inferred. Specify ControllerName in the attribute.",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static readonly DiagnosticDescriptor MissingInterfaces =
        new DiagnosticDescriptor(
            id: "VDM004",
            title: "No ICommandHandler or IQueryHandler interface",
            messageFormat: "The class '{0}' does not implement any ICommandHandler or IQueryHandler interfaces and cannot generate controllers",
            category: "ViixsSourceGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
}


internal class DiagnosticDescriptorReporter
{
    public static DiagnosticDescriptorReporter DuplicateControllerName = new DiagnosticDescriptorReporter(DiagnosticDescriptorHelper.DuplicateControllerName);
    public static DiagnosticDescriptorReporter MissingConcreteType = new DiagnosticDescriptorReporter(DiagnosticDescriptorHelper.MissingConcreteType);
    public static DiagnosticDescriptorReporter MissingControllerName = new DiagnosticDescriptorReporter(DiagnosticDescriptorHelper.MissingControllerName);
    public static DiagnosticDescriptorReporter MissingInterfaces = new DiagnosticDescriptorReporter(DiagnosticDescriptorHelper.MissingInterfaces);


    private DiagnosticDescriptorReporter(DiagnosticDescriptor descriptor)
    {
        //Should have default descriptor for when the actual descriptor is null ?
        _descriptor = descriptor; //?? throw new ArgumentNullException(nameof(descriptor));
    }

    private readonly DiagnosticDescriptor _descriptor;

    public void ReportDiagnostic(SourceProductionContext sourceProductionContext, GeneratedControllerData? controllerData, params object?[]? messageArguments)
    {
        var lineLocation = controllerData?.AttributeLocation ?? Location.None;
        if(messageArguments == null || messageArguments.Length == 0)
        {
            messageArguments = [controllerData?.TargetClassName ?? "UnknownHandler"];
        }
        var diagnostic = Diagnostic.Create(_descriptor, lineLocation, messageArguments);
        sourceProductionContext.ReportDiagnostic(diagnostic);
    }
}
