using MediatR;
using RythuMitraAI.Application.Common;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Queries.SearchFarmers;

/// <summary>
/// Query to search and page farmers by various filters.
/// </summary>
public sealed class SearchFarmersQuery : IRequest<PagedResponse<FarmerListResponse>>
{
    /// <summary>
    /// Page number (1-based).
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; init; } = 10;

    /// <summary>
    /// Generic search term applied to name, code or other searchable fields.
    /// </summary>
    public string? Search { get; init; }

    /// <summary>
    /// Filter by district.
    /// </summary>
    public string? District { get; init; }

    /// <summary>
    /// Filter by village.
    /// </summary>
    public string? Village { get; init; }

    /// <summary>
    /// Filter by state.
    /// </summary>
    public string? State { get; init; }
}
