using System;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Represents an agricultural crop associated with a farmer.
/// Inherits auditing information from <see cref="AuditableEntity"/>.
/// </summary>
public class Crop : AuditableEntity
{
    /// <summary>
    /// Unique code for the crop.
    /// </summary>
    public string CropCode { get; set; } = string.Empty;

    /// <summary>
    /// Human-friendly crop name.
    /// </summary>
    public string CropName { get; set; } = string.Empty;

    /// <summary>
    /// Category of the crop (e.g., cereal, vegetable, fruit).
    /// </summary>
    public string CropCategory { get; set; } = string.Empty;

    /// <summary>
    /// Season in which the crop is grown (e.g., Kharif, Rabi).
    /// </summary>
    public string Season { get; set; } = string.Empty;

    /// <summary>
    /// Date when the crop was sown.
    /// </summary>
    public DateTime SowingDate { get; set; }

    /// <summary>
    /// Expected or actual harvest date. Optional.
    /// </summary>
    public DateTime? HarvestDate { get; set; }

    /// <summary>
    /// Area under this crop.
    /// </summary>
    public decimal Area { get; set; }

    /// <summary>
    /// Unit of the area (e.g., acres, hectares).
    /// </summary>
    public string AreaUnit { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the farmer who owns this crop.
    /// </summary>
    public Guid FarmerId { get; set; }

    /// <summary>
    /// Indicates whether the crop record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Auditing properties (CreatedAt, CreatedBy, ModifiedAt, ModifiedBy) are inherited from AuditableEntity
}

