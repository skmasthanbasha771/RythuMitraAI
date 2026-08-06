using System;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Represents soil test data associated with a farmer's land.
/// Inherits auditing information from <see cref="AuditableEntity"/>.
/// </summary>
public class Soil : AuditableEntity
{
    /// <summary>
    /// Unique code for the soil sample.
    /// </summary>
    public string SoilCode { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the farmer who owns the land tested.
    /// </summary>
    public Guid FarmerId { get; set; }

    /// <summary>
    /// Soil pH value.
    /// </summary>
    public decimal PH { get; set; }

    /// <summary>
    /// Soil moisture percentage/measurement.
    /// </summary>
    public decimal Moisture { get; set; }

    /// <summary>
    /// Nitrogen content measurement.
    /// </summary>
    public decimal Nitrogen { get; set; }

    /// <summary>
    /// Phosphorus content measurement.
    /// </summary>
    public decimal Phosphorus { get; set; }

    /// <summary>
    /// Potassium content measurement.
    /// </summary>
    public decimal Potassium { get; set; }

    /// <summary>
    /// Organic carbon percentage/measurement.
    /// </summary>
    public decimal OrganicCarbon { get; set; }

    /// <summary>
    /// Date the soil test was performed.
    /// </summary>
    public DateTime TestDate { get; set; }

    /// <summary>
    /// Additional remarks or observations from the soil test.
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// Indicates whether the soil record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Auditing properties (CreatedAt, CreatedBy, ModifiedAt, ModifiedBy) are inherited from AuditableEntity
}
