using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Fertilizers.DTOs;

public sealed class CreateFertilizerRequest
{
    [Required]
    [MaxLength(50)]
    public string FertilizerCode { get; init; } = string.Empty;

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

    public bool IsActive { get; init; } = true;
}
