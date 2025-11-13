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
        HashSet<(string, string)> returnList = [];
        if (!controllerData.Any())
        {
            return returnList;
            //yield return (string.Empty, string.Empty);
        }

        var groupedControllers = controllerData.GroupBy(data => (data.GeneratedControllerName, data.GeneratedNamespace));
        var controllerName = string.Empty;

        foreach (var groupedController in groupedControllers)
        {
            var stringBuilder = new StringBuilder();
            controllerName = $"{groupedController.Key.GeneratedControllerName}RouteActions";
            var nameSpace = groupedController.Key.GeneratedNamespace;

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
                                var methodName = $"{method}{groupedController.Key.GeneratedControllerName}";

                                methodBodybuilder.AddLine($"builder.Map{method}(\"/{routeAction.Action}\", {methodName});");
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
                    var methodName = $"{method}{(string.IsNullOrEmpty(routeAction.Action) ? routeAction.GeneratedControllerName : routeAction.Action)}";
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

            returnList.Add((controllerName, classBuilder.Build().GetClassString()));
            //yield return (controllerName, classBuilder.Build().GetClassString());

            //var uniqueSignatures = new HashSet<string>();

            //foreach (var routeAction in groupedController)
            //{
            //    var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
            //    var methodName = $"{method}{routeAction.Action}";
            //    var isGet = method == HttpMethod.Get.Method.GetHttpMethodName();
            //    var methodReturnType = isGet ? $"Task<{routeAction.ReturnType}>" : "Task";

            //    var signature = GetMethodSignature(routeAction, method, isGet);

            //    if (!uniqueSignatures.Add(signature))
            //    {
            //        continue;
            //    }

            //    stringBuilder.AppendLine();
            //    //TODO: summary ?
            //    stringBuilder.AppendLine($"{Indent4}{signature}");
            //    stringBuilder.AppendLine($"{Indent4}{OpenBrace}");
            //    stringBuilder.AppendLine($"{Indent4}{Indent4}{BuildMethodBody(routeAction, method, isGet)}");
            //    stringBuilder.AppendLine($"{Indent4}{CloseBrace}");
            //}


            //stringBuilder.AppendLine("// <auto-generated />");
            //stringBuilder.AppendLine("#nullable enable");
            //stringBuilder.AppendLine("using Microsoft.AspNetCore.Builder;");
            //stringBuilder.AppendLine("using Microsoft.AspNetCore.Routing;");
            //stringBuilder.AppendLine("using Microsoft.AspNetCore.Mvc;");
            //stringBuilder.AppendLine("using ViixsDockerManager.Mediator;");
            //stringBuilder.AppendLine($"namespace {nameSpace};");
            //stringBuilder.AppendLine();
            //stringBuilder.AppendLine($"internal static class {controllerName}");
            //stringBuilder.AppendLine(OpenBrace);

            //stringBuilder.AppendLine($"{Indent4}internal static RouteGroupBuilder Map{controllerName}(this RouteGroupBuilder builder)");
            //stringBuilder.AppendLine($"{Indent4}{OpenBrace}");
            //foreach (var routeAction in groupedController)
            //{
            //    var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
            //    var methodName = $"{method}{groupedController.Key.GeneratedControllerName}";

            //    stringBuilder.AppendLine($"{Indent4}{Indent4}builder.Map{method}(\"/{routeAction.Action}\", {methodName});");
            //}
            //stringBuilder.AppendLine();
            //stringBuilder.AppendLine($"{Indent4}{Indent4}return builder;");
            //stringBuilder.AppendLine($"{Indent4}{CloseBrace}");

            //var uniqueSignatures = new HashSet<string>();

            //foreach (var routeAction in groupedController)
            //{
            //    var method = routeAction.GeneratedHttpMethodName.GetHttpMethodName();
            //    var methodName = $"{method}{routeAction.Action}";
            //    var isGet = method == HttpMethod.Get.Method.GetHttpMethodName();
            //    var methodReturnType = isGet ? $"Task<{routeAction.ReturnType}>" : "Task";

            //    var signature = GetMethodSignature(routeAction, method, isGet);

            //    if (!uniqueSignatures.Add(signature))
            //    {
            //        continue;
            //    }

            //    stringBuilder.AppendLine();
            //    //TODO: summary ?
            //    stringBuilder.AppendLine($"{Indent4}{signature}");
            //    stringBuilder.AppendLine($"{Indent4}{OpenBrace}");
            //    stringBuilder.AppendLine($"{Indent4}{Indent4}{BuildMethodBody(routeAction, method, isGet)}");
            //    stringBuilder.AppendLine($"{Indent4}{CloseBrace}");
            //}
            //stringBuilder.AppendLine(CloseBrace);
            //yield return (controllerName, stringBuilder.ToString());
        }
        return returnList;
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
