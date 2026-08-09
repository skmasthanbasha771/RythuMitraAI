using System;

namespace RythuMitraAI.Application.Fertilizers.DTOs;

public sealed class FertilizerResponse
{
    public Guid Id { get; init; }
    public string FertilizerCode { get; init; } = string.Empty;
    public string FertilizerName { get; init; } = string.Empty;
    public string Brand { get; init; } = string.Empty;
    public string FertilizerType { get; init; } = string.Empty;
    public decimal Nitrogen { get; init; }
    public decimal Phosphorus { get; init; }
    public decimal Potassium { get; init; }
    public string RecommendedCrop { get; init; } = string.Empty;
    public string RecommendedSoil { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
