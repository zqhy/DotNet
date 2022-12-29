namespace Hy.Extensions;

public static class PagingExtensions
{
    public static Paging<TR> Transform<T, TR>(this Paging<T> source, Func<T, TR> transform) =>
        new(source.Items.Select(transform).ToArray(), source.Page, source.PageSize, source.TotalCount);
}