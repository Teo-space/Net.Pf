using Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Extensions;

public static class ExtensionsRedirectToPageByType
{
    static string Path(Type? type)
    {
        string? nameSpace = type?.Namespace;
        string? assemblyName = type?.Assembly?.GetName()?.Name;

        string? root = nameSpace?.Replace(assemblyName ?? string.Empty, default)?.Replace(".Pages", default);
        string? modelName = type?.Name.Replace("Model", "");

        return $"{root}.{modelName}".Replace(".", "/");
    }

    static string Path<TModel>() where TModel : PageModel => Path(typeof(TModel));


    public static RedirectToPageResult RedirectToPageByType<TModel>(this PageModel model, object? routeValues)  where TModel : PageModel
    {
        return model.RedirectToPage(Path<TModel>(), routeValues);
    }

}

