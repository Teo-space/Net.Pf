using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Security.Policy;


namespace Net.Pf.Filters.PageFilters;


public class CounterPageFilter : IPageFilter
{
    public static IEnumerable<KeyValuePair<string, int>> GetCache() => Cashe.ToArray();

    static readonly ConcurrentDictionary<string, int> Cashe = new();

    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        try
        {
            //var id = $"DisplayName:{context.ActionDescriptor.DisplayName}.RelativePath:{context.ActionDescriptor.RelativePath}";
            var id = $"{context.ActionDescriptor.RelativePath}";

            if (Cashe.TryGetValue(id, out var value))
            {
                Cashe[id] = value + 1;
            }
            else Cashe[id] = 1;
        }
        catch (Exception ex)
        {
            context.Result = new ObjectResult($"Exception on CounterPageFilter: {ex.Message}") { StatusCode = 500 };
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    }
}
