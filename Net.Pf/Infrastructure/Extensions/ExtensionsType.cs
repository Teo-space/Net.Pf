using System.Collections.ObjectModel;


namespace Net.Pf.Infrastructure.Extensions;


public static class ExtensionsType
{
    public static Type Type<T>(this Collection<T> collection) => typeof(T);
    public static Type Type<T>(this IList<T> collection) => typeof(T);
    public static Type Type<T>(this IReadOnlyList<T> collection) => typeof(T);
    public static Type Type<T>(this List<T> collection) => typeof(T);
    public static Type Type<T>(this IEnumerable<T> collection) => typeof(T);








    public static Type Type(this object o) => o.GetType();



}
