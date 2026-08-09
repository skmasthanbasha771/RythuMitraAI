using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Fertilizers.DTOs;

/// <summary>
/// DTO for updating editable fertilizer fields.
/// </summary>
public sealed class UpdateFertilizerRequest
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    [MaxLength(150)]
    public string FertilizerName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Brand { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FertilizerType { get; init; } = string.Empty;

    [Required]
    public decimal Nitrogen { get; init; }

    [Required]
    public decimal Phosphorus { get; init; }

    [Required]
    public decimal Potassium { get; init; }

    [Required]
    [MaxLength(100)]
    public string RecommendedCrop { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string RecommendedSoil { get; init; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; init; }

    public bool IsActive { get; init; }
}
