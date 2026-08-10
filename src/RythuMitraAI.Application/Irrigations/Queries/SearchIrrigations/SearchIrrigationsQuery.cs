using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Irrigations.DTOs;

namespace RythuMitraAI.Application.Irrigations.Queries.SearchIrrigations;

/// <summary>
/// Query to search irrigations with pagination and filtering.
/// </summary>
public sealed class SearchIrrigationsQuery : IRequest<PagedResponse<IrrigationResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? IrrigationCode { get; init; }
    public Guid? FarmerId { get; init; }
    public Guid? CropId { get; init; }
    public string? IrrigationType { get; init; }
    public string? WaterSource { get; init; }
    public DateTime? IrrigationDate { get; init; }
    public bool? IsActive { get; init; }
}
