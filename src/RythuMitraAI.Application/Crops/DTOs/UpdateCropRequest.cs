using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Crops.DTOs;

/// <summary>
/// DTO used to update an existing crop.
/// </summary>
public sealed class UpdateCropRequest
{
    /// <summary>
    /// Crop name.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string CropName { get; init; } = string.Empty;

    /// <summary>
    /// Crop category.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string CropCategory { get; init; } = string.Empty;

    /// <summary>
    /// Season for the crop.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Season { get; init; } = string.Empty;

    /// <summary>
    /// Sowing date.
    /// </summary>
    [Required]
    public DateTime SowingDate { get; init; }

    /// <summary>
    /// Harvest date (optional).
    /// </summary>
    public DateTime? HarvestDate { get; init; }

    /// <summary>
    /// Area planted.
    /// </summary>
    [Range(0.01, double.MaxValue)]
    public decimal Area { get; init; }

    /// <summary>
    /// Unit of the area.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string AreaUnit { get; init; } = string.Empty;

    /// <summary>
    /// Identifier of the owning farmer.
    /// </summary>
    [Required]
    public Guid FarmerId { get; init; }

    /// <summary>
    /// Indicates whether the crop is active.
    /// </summary>
    public bool IsActive { get; init; } = true;
}
