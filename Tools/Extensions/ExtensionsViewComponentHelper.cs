using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;


namespace Tools.Extensions;


public static class ExtensionsViewComponentHelper
{
    public static async Task<IHtmlContent> Invoke<TComponent>(this IViewComponentHelper helper, object? arguments = default)
        where TComponent : ViewComponent
    {
        return await helper.InvokeAsync(typeof(TComponent), arguments);
    }

}
