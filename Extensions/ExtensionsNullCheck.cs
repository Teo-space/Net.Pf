namespace Extensions;

public static class ExtensionsNullCheck
{

    public static string NullCheck(this string value, string nameof)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof);
        }
        return value;
    }


    public static Guid NullCheck(this Guid guid, string nameof)
    {
        if (guid == Guid.Empty || string.IsNullOrEmpty(guid.ToString()))
        {
            throw new ArgumentNullException(nameof);
        }
        return guid;
    }
}
