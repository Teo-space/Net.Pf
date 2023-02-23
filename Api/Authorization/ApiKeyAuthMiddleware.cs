using Microsoft.AspNetCore.Mvc;

namespace Api.Authorization;


/// <summary>
/// appsettings.json -> "X-API-KEY": "QXBpS2V5TWlkZGxld2FyZQ=="
/// ApiKey = "X-API-KEY"
/// app.UseMiddleware<ApiKeyMiddleware>();
/// </summary>
public record ApiKeyAuthMiddleware(RequestDelegate requestDelegate)
{
    private const string ApiKey = "X-API-KEY";

    public async Task Invoke(HttpContext context)
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

        await requestDelegate(context);
    }


}


