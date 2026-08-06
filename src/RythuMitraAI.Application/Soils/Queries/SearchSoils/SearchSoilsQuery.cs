using System;
using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Soils.DTOs;

namespace RythuMitraAI.Application.Soils.Queries.SearchSoils;

/// <summary>
/// Query to search soils with filters and pagination.
/// </summary>
public sealed class SearchSoilsQuery : IRequest<PagedResponse<SoilResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public string? SoilCode { get; init; }
    public Guid? FarmerId { get; init; }
    public DateTime? TestDate { get; init; }

    public decimal? MinPH { get; init; }
    public decimal? MaxPH { get; init; }

    public decimal? MinNitrogen { get; init; }
    public decimal? MaxNitrogen { get; init; }

    public decimal? MinPhosphorus { get; init; }
    public decimal? MaxPhosphorus { get; init; }

    public decimal? MinPotassium { get; init; }
    public decimal? MaxPotassium { get; init; }
}
