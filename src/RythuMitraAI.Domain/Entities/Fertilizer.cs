using System;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Represents a fertilizer product with nutrient composition and recommendations.
/// Inherits auditing information from <see cref="AuditableEntity"/>.
/// </summary>
public class Fertilizer : AuditableEntity
{
    /// <summary>
    /// Unique code for the fertilizer.
    /// </summary>
    public string FertilizerCode { get; set; } = string.Empty;

    /// <summary>
    /// Human readable name of the fertilizer.
    /// </summary>
    public string FertilizerName { get; set; } = string.Empty;

    /// <summary>
    /// Brand or manufacturer name.
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Type of fertilizer (e.g., Organic, Inorganic, NPK, Urea).
    /// </summary>
    public string FertilizerType { get; set; } = string.Empty;

    /// <summary>
    /// Nitrogen content.
    /// </summary>
    public decimal Nitrogen { get; set; }

    /// <summary>
    /// Phosphorus content.
    /// </summary>
    public decimal Phosphorus { get; set; }

    /// <summary>
    /// Potassium content.
    /// </summary>
    public decimal Potassium { get; set; }

    /// <summary>
    /// Recommended crop(s) for this fertilizer.
    /// </summary>
    public string RecommendedCrop { get; set; } = string.Empty;

    /// <summary>
    /// Recommended soil type(s) for this fertilizer.
    /// </summary>
    public string RecommendedSoil { get; set; } = string.Empty;

    /// <summary>
    /// Additional description or usage instructions.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the fertilizer record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Auditable properties (CreatedAt, CreatedBy, ModifiedAt, ModifiedBy) are inherited from AuditableEntity
}
