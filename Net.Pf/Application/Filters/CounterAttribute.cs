using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;



namespace Net.Pf.Application.Filters;



internal class CounterAttribute : ActionFilterAttribute
{
    public static readonly ConcurrentDictionary<string, int> Cashe = new();

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            string id = $"{context.RouteData.Values["Controller"]}.{context.HttpContext.Request.Method}.{context.RouteData.Values["action"]}";

            if (Cashe.TryGetValue(id, out int value))
            {
                Cashe[id] = value + 1;
            }
            else Cashe[id] = 1;

        }
        catch (Exception ex)
        {
            context.Result = new ObjectResult($"Exception on RateLimitAttribute: {ex.Message}") { StatusCode = 500 };
        }

        base.OnActionExecuting(context);
    }
}
