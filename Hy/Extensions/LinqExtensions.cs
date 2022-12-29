namespace Hy.Extensions;

public static class LinqExtensions
{
    /// <summary>
    /// 去重复
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        var seenKeys = new HashSet<TKey>();
        return from element in source let elementValue = keySelector(element) where seenKeys.Add(elementValue) select element;
    }
}