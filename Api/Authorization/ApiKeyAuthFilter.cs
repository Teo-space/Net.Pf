namespace Api.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

public record ApiKeyAuthFilter(IConfiguration Configuration)

    //: IAsyncAuthorizationFilter
    : IAuthorizationFilter
{
    private const string ApiKey = "X-API-KEY";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
        {
            context.Result = new UnauthorizedObjectResult("Api Key not found!");
        }

        //var Configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = Configuration.GetValue<string>(ApiKey);
        if (!apiKey.Equals(apiKeyVal))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized client");
        }
    }


}


