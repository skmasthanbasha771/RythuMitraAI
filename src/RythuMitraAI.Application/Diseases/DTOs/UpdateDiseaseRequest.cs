using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Diseases.DTOs;

/// <summary>
/// DTO used to update an existing disease.
/// </summary>
public sealed class UpdateDiseaseRequest
{
    [Required]
    [MaxLength(50)]
    public string DiseaseCode { get; init; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string DiseaseName { get; init; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string CropType { get; init; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Symptoms { get; init; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Causes { get; init; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Treatment { get; init; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Prevention { get; init; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Severity { get; init; } = string.Empty;

    public bool IsActive { get; init; } = true;
}
