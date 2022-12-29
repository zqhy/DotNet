namespace Hy;

public record PagingParams<TK>(TK Page, int? PageSize);

public record PagingParams(int? Page, int? PageSize) : PagingParams<int?>(Page, PageSize);

public record Paging(int Page, int PageSize, int TotalCount, string? EmptyTips = null)
{
    public int TotalPages
    {
        get
        {
            if (PageSize > 0)
            {
                var totalPages = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                {
                    totalPages += 1;
                }
                return totalPages;
            }
            return 0;
        }
    }
    
    public int? NextPage => Page >= TotalPages ? null : Page + 1;
    public int? PreviousPage => Page == 1 || TotalPages <= 1 ? null : Page - 1;
    
    public int StarPage => (Page - 1) * PageSize + 1;
    public int EndPage => Math.Min(Page * PageSize, TotalCount);
}

public record Paging<TK, T>(TK Page, TK? NextPage, TK? PreviousPage, T[] Items, int PageSize, int TotalCount, string? EmptyTips)
{
    public int TotalPages
    {
        get
        {
            if (PageSize > 0)
            {
                var totalPages = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                {
                    totalPages += 1;
                }
                return totalPages;
            }
            return 0;
        }
    }
}

public record Paging<T>(T[] Items, int Page, int PageSize, int TotalCount, string? EmptyTips = null) : Paging(Page, PageSize, TotalCount, EmptyTips);