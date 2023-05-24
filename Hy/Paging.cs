using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hy;

public record PagingParams
{
    [Display(Name = "页码")]
    public int? Page { get; }
    
    [Display(Name = "每页记录数")]
    public int? PageSize { get; }

    public PagingParams(int? page, int? pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}

public record Paging
{
    [Display(Name = "页码")]
    public int Page { get; }
    
    [Display(Name = "每页记录数")]
    public int PageSize { get; }
    
    [Display(Name = "记录数")]
    public int TotalCount { get; }

    public Paging(int page, int pageSize, int totalCount)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    [JsonIgnore]
    [Display(Name = "总页数")]
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
    
    [Display(Name = "下一页")]
    public int? NextPage => Page >= TotalPages ? null : Page + 1;
    
    [JsonIgnore]
    [Display(Name = "上一页")]
    public int? PreviousPage => Page == 1 || TotalPages <= 1 ? null : Page - 1;
    
    [JsonIgnore]
    [Display(Name = "开始页码")]
    public int StarPage => (Page - 1) * PageSize + 1;
    [JsonIgnore]
    [Display(Name = "结束页码")]
    public int EndPage => Math.Min(Page * PageSize, TotalCount);
}

public record Paging<T> : Paging
{
    [Display(Name = "分页数据")]
    public T[] Items { get; }

    public Paging(T[] items, int page, int pageSize, int totalCount) : base(page, pageSize, totalCount)
    {
        Items = items;
    }

    public static Paging<T> Empty(int page, int pageSize) => new(Array.Empty<T>(), page, pageSize, 0);
}