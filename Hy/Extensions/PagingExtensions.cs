namespace Hy.Extensions;

public static class PagingExtensions
{
    public static Paging<Tr> Transform<T, Tr>(this Paging<T> source, Func<T, Tr> transform) =>
        new(source.Items.Select(transform).ToArray(), source.Page, source.PageSize, source.TotalCount);
}