using System;
using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Crops.DTOs;

namespace RythuMitraAI.Application.Crops.Queries.SearchCrops;

/// <summary>
/// Query to search crops with pagination and filtering.
/// </summary>
public sealed class SearchCropsQuery : IRequest<PagedResponse<CropResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? CropName { get; init; }
    public string? CropCategory { get; init; }
    public string? Season { get; init; }
    public Guid? FarmerId { get; init; }
}
