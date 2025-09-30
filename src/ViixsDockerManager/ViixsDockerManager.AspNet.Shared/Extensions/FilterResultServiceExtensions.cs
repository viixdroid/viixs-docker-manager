using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ViixsDockerManager.Shared.AspNet.Filters;

namespace ViixsDockerManager.Shared.AspNet.Extensions;

public static class FilterResultServiceExtensions
{
    public static IEndpointRouteBuilder ApplyEndpointFilter(this IEndpointRouteBuilder routeBuilder)
    {
        return routeBuilder.MapGroup(string.Empty)
            .AddEndpointFilter<ResponseObjectEndpointFilter>();
    }
}
