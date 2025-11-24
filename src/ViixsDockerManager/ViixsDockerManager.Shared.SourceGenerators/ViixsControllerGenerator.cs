using System.Text;
using ViixsDockerManager.Shared.SourceGenerators.Builders;
using ViixsDockerManager.Shared.SourceGenerators.Builders.Models;
using ViixsDockerManager.Shared.SourceGenerators.Helpers;
using ViixsDockerManager.Shared.SourceGenerators.Models;

namespace ViixsDockerManager.Shared.SourceGenerators;

internal class ViixsControllerGenerator
{
    private const string OpenBrace = "{";
    private const string CloseBrace = "}";
    private const string Indent4 = "    ";
    internal static IEnumerable<(string controllerName, string sourceCode)> GenerateControllerSource(IEnumerable<GeneratedControllerData> controllerData)
    {
        const string defaultNamespace = "ViixsDockerManager.GeneratedControllers"; //TODO: move to const

        var results = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!controllerData.Any())
        {
            return results.Select(kv => (kv.Key, kv.Value));
        }

        var groupedControllers = controllerData.GroupBy(data => data.GeneratedControllerName);
        var controllerName = string.Empty;

        foreach (var groupedController in groupedControllers)
        {
            var stringBuilder = new StringBuilder();
            controllerName = $"{groupedController.Key}RouteActions";
            var nameSpace = groupedController.Select(g => g.GeneratedNamespace).FirstOrDefault() ?? defaultNamespace;

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
            foreach (var routeAction in groupedController)
            {
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

    private static string GetMethodSignature(GeneratedControllerData routeAction, string method, bool isGet)
    {
        var methodName = $"{method}{routeAction.GeneratedControllerName}";
        var methodReturnType = isGet ? $"Task<{routeAction.ReturnType}>" : "Task";

        var signature = $"private static {methodReturnType} {methodName}({(isGet ? string.Empty : $"[FromBody]{routeAction.ParameterType} command, ")}[FromServices] IMediator mediator)";
        return signature;
    }

    private static string BuildMethodBody(GeneratedControllerData routeAction, string method, bool isGet)
    {
        if (!isGet)
        {
            return $"return mediator.Send(command);";
        }
        return
           $"""
            var query = new {routeAction.ParameterType}();
            {Indent4}{Indent4}return mediator.Send(new {routeAction.ParameterType}());
            """;
    }
}
