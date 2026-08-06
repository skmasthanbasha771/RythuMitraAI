using System;

namespace RythuMitraAI.Application.Soils.DTOs;

/// <summary>
/// DTO returned for soil entities.
/// </summary>
public sealed class SoilResponse
{
    public Guid Id { get; init; }
    public string SoilCode { get; init; } = string.Empty;
    public Guid FarmerId { get; init; }
    public decimal PH { get; init; }
    public decimal Moisture { get; init; }
    public decimal Nitrogen { get; init; }
    public decimal Phosphorus { get; init; }
    public decimal Potassium { get; init; }
    public decimal OrganicCarbon { get; init; }
    public DateTime TestDate { get; init; }
    public string? Remarks { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
