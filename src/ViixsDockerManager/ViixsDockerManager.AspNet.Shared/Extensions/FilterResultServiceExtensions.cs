using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ViixDockerManager.AspNet.Shared.Filters;

namespace ViixDockerManager.AspNet.Shared.Extensions;

public static class FilterResultServiceExtensions
{
    public static IEndpointRouteBuilder ApplyEndpointFilter(this IEndpointRouteBuilder routeBuilder)
    {
        return routeBuilder.MapGroup(string.Empty)
            .AddEndpointFilter<ResponseObjectEndpointFilter>();
    }
}
