using System;
using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.Application.Fertilizers.Queries.SearchFertilizers;

public sealed class SearchFertilizersQuery : IRequest<PagedResponse<FertilizerResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public string? FertilizerCode { get; init; }
    public string? FertilizerName { get; init; }
    public string? Brand { get; init; }
    public string? FertilizerType { get; init; }
    public string? RecommendedCrop { get; init; }
    public string? RecommendedSoil { get; init; }
}
