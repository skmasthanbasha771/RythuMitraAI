namespace RythuMitraAI.Application.Common;

public sealed class PaginationResponse<T>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public long TotalItems { get; init; }
    public int TotalPages { get; init; }
    public IEnumerable<T> Items { get; init; } = Array.Empty<T>();

    public PaginationResponse(IEnumerable<T> items, int page, int pageSize, long totalItems)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }
}
