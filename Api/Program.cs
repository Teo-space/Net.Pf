using Api.Authorization;
using Microsoft.AspNetCore.Builder;

WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder().Build()
    .ConfigureApp().Run()
    ;

public static class WebApplicationBuilderConfiguration
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        //builder.Services.AddTransient<ApiKeyAuthMiddlewareIMiddleware>();
        builder.Services.AddControllers();
        //builder.Services.AddControllers(options => options.Filters.Add<ApiKeyAuthFilter>());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenConfigureApiKeyAuth();

        return builder;
    }
}

public static class WebApplicationConfiguration
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        //app.UseMiddleware<ApiKeyAuthMiddlewareIMiddleware>();
        //app.UseMiddleware<ApiKeyMiddleware>();
        app.UseAuthorization();
        app.MapControllers();

        //app.MapGet("/Hello", () => "Hello World!").RequireAuthorization();
        //app.MapGet("/Hello", (string a) => $"Hello World {a}").RequireAuthorization();


        //app.Route("Fluent").WithArgument("test").Build();

        return app;
    }
}


public static class ExtensionsEndpointConventionBuilder
{
    public static EndpointConventionBuilder Route
        (this IEndpointRouteBuilder endpoints, string route) => new EndpointConventionBuilder(endpoints, route);


}


public record EndpointConventionBuilder(IEndpointRouteBuilder endpoints, string Route)
{
    public EndpointConventionBuilder Create(string route) => new(endpoints, route);

    public EndpointConventionBuilderWithArgument<T> WithArgument<T>(T arg) 
        => new EndpointConventionBuilderWithArgument<T>(endpoints, Route, arg);
}




public record EndpointConventionBuilderWithArgument<TArgument>
    (IEndpointRouteBuilder endpoints, string Route, TArgument Argument) 
    : EndpointConventionBuilder(endpoints, Route)
{
    public IEndpointConventionBuilder Build()
    {
        return endpoints.MapGet(Route, (TArgument Argument) => $"Res: {Argument}");
    }
}



public record EndpointConventionBuilderResult<TArgument>

    (IEndpointRouteBuilder endpoints, string Route, TArgument Argument)

    : EndpointConventionBuilderWithArgument<TArgument>(endpoints, Route, Argument)

{
    public IEndpointConventionBuilder Build<TBuilder>() where TBuilder : IEndpointConventionBuilder
    {
        return endpoints.MapGet(Route, (TArgument Argument) => $"Res: {Argument}");
    }


}
