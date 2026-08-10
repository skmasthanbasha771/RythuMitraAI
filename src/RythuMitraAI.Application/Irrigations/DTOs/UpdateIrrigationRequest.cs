using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Irrigations.DTOs;

/// <summary>
/// DTO used to update an existing irrigation record.
/// </summary>
public sealed class UpdateIrrigationRequest
{
    [Required]
    [MaxLength(50)]
    public string IrrigationCode { get; init; } = string.Empty;

    [Required]
    public Guid FarmerId { get; init; }

    [Required]
    public Guid CropId { get; init; }

    [Required]
    [MaxLength(100)]
    public string IrrigationType { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string WaterSource { get; init; } = string.Empty;

    [Required]
    public DateTime IrrigationDate { get; init; }

    [Required]
    public int DurationInMinutes { get; init; }

    [Required]
    public decimal WaterQuantity { get; init; }

    [Required]
    [MaxLength(50)]
    public string WaterUnit { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Remarks { get; init; }

    public bool IsActive { get; init; } = true;
}
