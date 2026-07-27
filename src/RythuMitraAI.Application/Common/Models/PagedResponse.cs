using System;
using System.Collections.Generic;

namespace RythuMitraAI.Application.Common.Models;

/// <summary>
/// Generic paged response model used by query handlers to return paged data sets.
/// </summary>
public sealed class PagedResponse<T>
{
    /// <summary>
    /// The items for the current page.
    /// </summary>
    public IEnumerable<T> Items { get; init; } = Array.Empty<T>();

    /// <summary>
    /// Current page number (1-based).
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Page size (items per page).
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Total number of records across all pages.
    /// </summary>
    public long TotalRecords { get; init; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="PagedResponse{T}"/>.
    /// </summary>
    public PagedResponse()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="PagedResponse{T}"/> and computes total pages.
    /// </summary>
    public PagedResponse(IEnumerable<T> items, int pageNumber, int pageSize, long totalRecords)
    {
        Items = items ?? Array.Empty<T>();
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)Math.Max(1, pageSize));
    }
}
