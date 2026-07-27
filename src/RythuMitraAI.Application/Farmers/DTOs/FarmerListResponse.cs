using System.Collections.Generic;

namespace RythuMitraAI.Application.Farmers.DTOs;

/// <summary>
/// Response containing a list of farmers and optional total count for paging.
/// </summary>
public sealed class FarmerListResponse
{
    /// <summary>
    /// Gets the list of farmer items.
    /// </summary>
    public IEnumerable<FarmerResponse> Items { get; init; } = System.Array.Empty<FarmerResponse>();

    /// <summary>
    /// Total number of farmers available (optional, used for paging).
    /// </summary>
    public long? TotalCount { get; init; }
}
