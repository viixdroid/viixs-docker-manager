using System.Text;
using ViixsDockerManager.Shared.SourceGenerators.Builders;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;
using ViixsDockerManager.Shared.SourceGenerators.Helpers;
using ViixsDockerManager.Shared.SourceGenerators.Models;

namespace ViixsDockerManager.Shared.SourceGenerators;

internal class ViixsControllerGenerator
{
    internal static IEnumerable<(string controllerName, string sourceCode)> GenerateControllerSource(IEnumerable<GeneratedControllerData> controllerData)
    {
        const string defaultNamespace = "ViixsDockerManager.GeneratedControllers"; //TODO: move to const

        var results = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Convert input to a list with an imperative pass to avoid multiple enumerations
        var dataList = controllerData is IList<GeneratedControllerData> list ? list : new List<GeneratedControllerData>(controllerData);

        if (dataList.Count == 0)
        {
            return results.Select(kv => (kv.Key, kv.Value));
        }

        // Group controllerData by GeneratedControllerName using an explicit dictionary to avoid LINQ allocations
        var groupedControllers = new Dictionary<string, List<GeneratedControllerData>>(StringComparer.OrdinalIgnoreCase);
        foreach (var d in dataList)
        {
            var key = d.GeneratedControllerName ?? string.Empty;
            if (!groupedControllers.TryGetValue(key, out var listForKey))
            {
                listForKey = new List<GeneratedControllerData>();
                groupedControllers[key] = listForKey;
            }
            listForKey.Add(d);
        }

        foreach (var kv in groupedControllers)
        {
            var groupedController = kv.Value;

            var stringBuilder = new StringBuilder();
            var controllerName = $"{kv.Key}RouteActions";

            // Determine namespace: use the first available GeneratedNamespace or fallback
            var nameSpace = defaultNamespace;
            if (groupedController.Count > 0 && !string.IsNullOrWhiteSpace(groupedController[0].GeneratedNamespace))
            {
                nameSpace = groupedController[0].GeneratedNamespace;
            }

            if (results.ContainsKey(controllerName))
            {
                continue;
            }

            var classBuilder = ClassBuilder.Create()
                .WithNameSpace(nameSpace)
                .WithUsing("Microsoft.AspNetCore.Builder")
                .WithUsing("Microsoft.AspNetCore.Routing")
                .WithUsing("Microsoft.AspNetCore.Mvc")
                .WithUsing("ViixsDockerManager.Mediator")
                .WithModifier(Modifier.Internal)
                .WithModifier(Modifier.Static)
                .WithClassName(controllerName)
                .WithMethod(methodBuilder =>
                {
                    methodBuilder
                        .WithModifier(Modifier.Internal)
                        .WithModifier(Modifier.Static)
                        .WithReturnType("RouteGroupBuilder")
                        .WithName($"Map{controllerName}")
                        .WithParameter("builder", "RouteGroupBuilder", true)
                        .WithBody(methodBodybuilder =>
                        {
                            foreach (var routeAction in groupedController)
                            {
                                var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
                                var methodName = GenerateMethodName(method, routeAction);

                                methodBodybuilder.AddLine($"builder.Map{method}(\"/{routeAction.Action.ToLowerInvariant()}\", {methodName});");
                            }
                            methodBodybuilder.AddLine();
                            methodBodybuilder.AddLine($"return builder;");
                        });
                });

            // For each routeAction, compute a best-effort FullTargetTypeName now (deferred expensive formatting)
            for (var i = 0; i < groupedController.Count; i++)
            {
                var routeAction = groupedController[i];

                if (string.IsNullOrWhiteSpace(routeAction.FullTargetTypeName))
                {
                    // Try to infer the handler namespace by removing a trailing ".Controllers" from the generated namespace
                    var handlerNamespace = routeAction.GeneratedNamespace ?? string.Empty;
                    const string controllersSuffix = ".Controllers";
                    if (handlerNamespace.EndsWith(controllersSuffix, StringComparison.Ordinal))
                    {
                        handlerNamespace = handlerNamespace.Substring(0, handlerNamespace.Length - controllersSuffix.Length);
                    }

                    var fullTargetName = string.IsNullOrWhiteSpace(handlerNamespace)
                        ? routeAction.TargetClassName
                        : handlerNamespace + "." + routeAction.TargetClassName;

                    // Replace the struct with a new instance containing the computed full name
                    groupedController[i] = new GeneratedControllerData(
                        routeAction.Action,
                        routeAction.TargetClassName,
                        fullTargetName,
                        routeAction.GeneratedControllerName,
                        routeAction.GeneratedNamespace ?? string.Empty,
                        routeAction.GeneratedHttpMethodName,
                        routeAction.ParameterType,
                        routeAction.ReturnType,
                        routeAction.AttributeLocation);

                    routeAction = groupedController[i];
                }

                // Add detailed methods for each routeAction
                classBuilder.WithMethod(methodBuilder =>
                {
                    var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
                    var methodName = GenerateMethodName(method, routeAction);

                    var isGet = method == HttpMethod.Get.Method.GetHttpMethodName();
                    var methodReturnType = isGet ? $"Task<{routeAction.ReturnType}>" : "Task";
                    var builder = methodBuilder
                        .WithModifier(Modifier.Private)
                        .WithModifier(Modifier.Static)
                        .WithReturnType(methodReturnType)
                        .WithName(methodName);
                    if (!isGet)
                    {
                        builder
                            .WithParameter("command", $"[FromBody] {routeAction.ParameterType}")
                            .WithBody(methodBodyBuilder => methodBodyBuilder.AddLine("return mediator.Send(command);"));
                    }
                    else
                    {
                        builder.WithBody(methodBodyBuilder =>
                        {
                            methodBodyBuilder.AddLine($"return mediator.Send(new {routeAction.ParameterType}());");
                        });
                    }
                    builder
                        .WithParameter("mediator", "[FromServices] IMediator");
                });
            }

            var source = classBuilder.Build().GetClassString();
            results.Add(controllerName, source);
        }
        return results.Select(kv => (kv.Key, kv.Value));
    }

    private static string GenerateMethodName(string httpMethod, GeneratedControllerData routeAction)
    {
        //var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
        var objectName = string.IsNullOrEmpty(routeAction.Action) ? routeAction.GeneratedControllerName : routeAction.Action;
        if (!char.IsUpper(objectName[0]))
        {
            objectName = char.ToUpperInvariant(objectName[0]) + objectName.Substring(1);
        }

        return $"{httpMethod}{objectName}";
    }
}
