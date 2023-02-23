using HtmlTags;

namespace Net.Pf.Infrastructure.Extensions;


public static class ExtensionsObject
{
    public static List<KeyValuePair<string, string>> Properties(this object o)
    {
        List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();

        var props = o.GetType().GetProperties();
        foreach (var prop in props)
        {
            var value = prop?.GetValue(o)?.ToString();
            results.Add(new KeyValuePair<string, string>(prop.Name, value ?? string.Empty));
        }
        return results;
    }
}
