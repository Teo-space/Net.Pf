using System.Text.Json;

namespace Tools.Serialization;

internal static class JsonS
{
    public static string Serealize<T>(T o)
    {
        if (ReferenceEquals(o, default(T)))
        {
            throw new ArgumentNullException("T o");
        }

        return JsonSerializer.Serialize(o);
    }
    public static T? Deserealize<T>(string json) where T : class
    {
        if (string.IsNullOrEmpty(json))
        {
            throw new ArgumentNullException("string json");
        }

        return JsonSerializer.Deserialize<T>(json);
    }


}
