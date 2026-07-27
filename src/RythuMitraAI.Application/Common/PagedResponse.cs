using System.Collections.Generic;

namespace RythuMitraAI.Application.Common;

/// <summary>
/// Generic paged response used for list endpoints.
/// </summary>
public sealed class PagedResponse<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public long TotalItems { get; init; }
    public int TotalPages { get; init; }
    public IEnumerable<T> Items { get; init; } = System.Array.Empty<T>();

    public PagedResponse()
    {
    }

    public PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, long totalItems)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
    }
}
