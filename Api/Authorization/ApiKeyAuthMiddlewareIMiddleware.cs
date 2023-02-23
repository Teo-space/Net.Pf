namespace Api.Authorization;


/// <summary>
/// 
/// builder.Services.AddTransient<ApiKeyMiddlewareIMiddleware>();
/// app.UseMiddleware<ApiKeyMiddlewareIMiddleware>();
/// </summary>
public record ApiKeyAuthMiddlewareIMiddleware() : IMiddleware
{
    private const string ApiKey = "X-API-KEY";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Api Key not found!");
        }

        var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = appSettings.GetValue<string>(ApiKey);

        if (!apiKey.Equals(apiKeyVal))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized client");
        }

        await next(context);
    }

}

