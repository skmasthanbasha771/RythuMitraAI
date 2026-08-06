using System;

namespace RythuMitraAI.Application.Crops.DTOs;

/// <summary>
/// Response returned after creating a crop.
/// </summary>
public sealed class CropResponse
{
    /// <summary>
    /// Crop identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Unique crop code.
    /// </summary>
    public string CropCode { get; init; } = string.Empty;

    /// <summary>
    /// Crop name.
    /// </summary>
    public string CropName { get; init; } = string.Empty;

    /// <summary>
    /// Crop category.
    /// </summary>
    public string CropCategory { get; init; } = string.Empty;

    /// <summary>
    /// Season for the crop.
    /// </summary>
    public string Season { get; init; } = string.Empty;

    /// <summary>
    /// Sowing date.
    /// </summary>
    public DateTime SowingDate { get; init; }

    /// <summary>
    /// Harvest date (optional).
    /// </summary>
    public DateTime? HarvestDate { get; init; }

    /// <summary>
    /// Area planted.
    /// </summary>
    public decimal Area { get; init; }

    /// <summary>
    /// Unit of the area.
    /// </summary>
    public string AreaUnit { get; init; } = string.Empty;

    /// <summary>
    /// Identifier of the owning farmer.
    /// </summary>
    public Guid FarmerId { get; init; }

    /// <summary>
    /// Whether the crop is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Creation time (UTC).
    /// </summary>
    public DateTime CreatedAtUtc { get; init; }

    /// <summary>
    /// Optional informational message.
    /// </summary>
    public string? Message { get; init; }
}
