namespace Hy.Extensions;

public static class EnumerableExtensions
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }
        
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? source) => !source.IsNullOrEmpty();
        
    public static string ToString<T>(this IEnumerable<T> source, string separator) => string.Join(separator, source);
        
    public static bool Equal<T>(this IEnumerable<T>? first, IEnumerable<T>? second) =>
        first == null && second == null || first != null && second != null && first.SequenceEqual(second);
}