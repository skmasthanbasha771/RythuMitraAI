using System;

namespace RythuMitraAI.Application.Irrigations.DTOs;

/// <summary>
/// Response returned after creating or retrieving an irrigation record.
/// </summary>
public sealed class IrrigationResponse
{
    public Guid Id { get; init; }

    public string IrrigationCode { get; init; } = string.Empty;

    public Guid FarmerId { get; init; }

    public Guid CropId { get; init; }

    public string IrrigationType { get; init; } = string.Empty;

    public string WaterSource { get; init; } = string.Empty;

    public DateTime IrrigationDate { get; init; }

    public int DurationInMinutes { get; init; }

    public decimal WaterQuantity { get; init; }

    public string WaterUnit { get; init; } = string.Empty;

    public string? Remarks { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public string? Message { get; init; }
}
