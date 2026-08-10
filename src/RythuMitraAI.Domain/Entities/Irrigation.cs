using System;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Domain entity representing an irrigation event performed for a crop by a farmer.
/// Inherits auditing properties from <see cref="AuditableEntity"/>.
/// </summary>
public class Irrigation : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique irrigation code.
    /// </summary>
    public string IrrigationCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the farmer who performed the irrigation.
    /// </summary>
    public Guid FarmerId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the crop that received irrigation.
    /// </summary>
    public Guid CropId { get; set; }

    /// <summary>
    /// Gets or sets the type of irrigation (e.g., Drip, Sprinkler, Flood).
    /// </summary>
    public string IrrigationType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source of water used for irrigation (e.g., Borewell, Canal).
    /// </summary>
    public string WaterSource { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the irrigation was performed.
    /// </summary>
    public DateTime IrrigationDate { get; set; }

    /// <summary>
    /// Gets or sets the duration of irrigation in minutes.
    /// </summary>
    public int DurationInMinutes { get; set; }

    /// <summary>
    /// Gets or sets the quantity of water used.
    /// </summary>
    public decimal WaterQuantity { get; set; }

    /// <summary>
    /// Gets or sets the unit used for WaterQuantity (e.g., liters, cubic_meters).
    /// </summary>
    public string WaterUnit { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets any remarks or notes about the irrigation event.
    /// </summary>
    public string Remarks { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the irrigation record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
