using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Diseases.DTOs;

namespace RythuMitraAI.Application.Diseases.Queries.SearchDiseases;

/// <summary>
/// Query to search diseases with pagination and filtering.
/// </summary>
public sealed class SearchDiseasesQuery : IRequest<PagedResponse<DiseaseResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? DiseaseCode { get; init; }
    public string? DiseaseName { get; init; }
    public string? CropType { get; init; }
    public string? Severity { get; init; }
    public bool? IsActive { get; init; }
}
