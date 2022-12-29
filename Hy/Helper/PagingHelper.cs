namespace Hy.Helper;

public static class PagingHelper
{
    public static Paging<T> GetPaging<T>(T[] items, int pageIndex, int pageSize, int totalCount)
    {
        var page = pageIndex + 1;
        return new Paging<T>(items, page, pageSize, totalCount);
    }
}