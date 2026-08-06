using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Soils.DTOs;

/// <summary>
/// DTO for creating a new soil record.
/// </summary>
public sealed class CreateSoilRequest
{
    [Required]
    [MaxLength(50)]
    public string SoilCode { get; init; } = string.Empty;

    [Required]
    public Guid FarmerId { get; init; }

    [Required]
    public decimal PH { get; init; }

    [Required]
    public decimal Moisture { get; init; }

    [Required]
    public decimal Nitrogen { get; init; }

    [Required]
    public decimal Phosphorus { get; init; }

    [Required]
    public decimal Potassium { get; init; }

    [Required]
    public decimal OrganicCarbon { get; init; }

    [Required]
    public DateTime TestDate { get; init; }

    [MaxLength(500)]
    public string? Remarks { get; init; }

    public bool IsActive { get; init; } = true;
}
