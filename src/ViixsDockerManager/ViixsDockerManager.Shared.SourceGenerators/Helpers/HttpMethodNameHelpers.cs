using System;
using System.Collections.Generic;
using System.Text;

namespace ViixsDockerManager.Shared.SourceGenerators.Helpers;

internal static class HttpMethodNameHelpers
{
    public static string GetHttpMethodName(this string methodName)
    {
        return methodName.ToUpperInvariant() switch
        {
            "GET" => "Get",
            "POST" => "Post",
            "PUT" => "Put",
            "DELETE" => "Delete",
            "PATCH" => "Patch",
            "HEAD" => "Head",
            "OPTIONS" => "Options",
            _ => throw new ArgumentException($"Unsupported HTTP method name: {methodName}", nameof(methodName))
        };
    }
}
