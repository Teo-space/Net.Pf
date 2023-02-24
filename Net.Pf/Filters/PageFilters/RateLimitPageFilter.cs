using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;


namespace Net.Pf.Filters.PageFilters;


public class RateLimitPageFilter : IPageFilter
{
    static readonly ConcurrentDictionary<string, int> Cashe = new();

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {

        //Http.Forwarded(context.HttpContext.Request)

        //Http.RemoteIp(context.HttpContext.Response)
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}
