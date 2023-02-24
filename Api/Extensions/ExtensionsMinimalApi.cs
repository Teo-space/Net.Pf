using System.Diagnostics.CodeAnalysis;

namespace Api.Extensions;

public static class ExtensionsMinimalApi
{
    public record Get(IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern);


    public static Get RouteGet(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
        => new Get(endpoints, pattern);


    public static IEndpointConventionBuilder Request(this Get c, Delegate requestDelegate)
        => c.endpoints.MapGet(c.pattern, requestDelegate);



    public record Post(IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern);


    public static Post RoutePost(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
        => new Post(endpoints, pattern);


    public static IEndpointConventionBuilder Request(this Post c, Delegate requestDelegate)
        => c.endpoints.MapPost(c.pattern, requestDelegate);



    public record Put(IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern);


    public static Put RoutePut(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
        => new Put(endpoints, pattern);


    public static IEndpointConventionBuilder Request(this Put c, Delegate requestDelegate)
        => c.endpoints.MapPut(c.pattern, requestDelegate);



    public record Patch(IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern);


    public static Patch RoutePatch(this IEndpointRouteBuilder endpoints, [StringSyntax("Route")] string pattern)
        => new Patch(endpoints, pattern);


    public static IEndpointConventionBuilder Request(this Patch c, Delegate requestDelegate)
        => c.endpoints.MapPatch(c.pattern, requestDelegate);
}

