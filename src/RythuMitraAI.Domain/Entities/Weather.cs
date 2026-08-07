using System;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

public class Weather : AuditableEntity
{
    /// <summary>
    /// Unique code for the weather record.
    /// </summary>
    public string WeatherCode { get; set; } = string.Empty;

    /// <summary>
    /// Identifier of the farmer associated with this weather observation.
    /// </summary>
    public Guid FarmerId { get; set; }

    /// <summary>
    /// Date and time of the weather observation.
    /// </summary>
    public DateTime WeatherDate { get; set; }

    /// <summary>
    /// Temperature measurement.
    /// </summary>
    public decimal Temperature { get; set; }

    /// <summary>
    /// Humidity measurement.
    /// </summary>
    public decimal Humidity { get; set; }

    /// <summary>
    /// Rainfall measurement.
    /// </summary>
    public decimal Rainfall { get; set; }

    /// <summary>
    /// Wind speed measurement.
    /// </summary>
    public decimal WindSpeed { get; set; }

    /// <summary>
    /// Short description of weather condition (e.g., Sunny, Cloudy).
    /// </summary>
    public string WeatherCondition { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the weather record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Navigation property to the farmer who owns this observation.
    /// </summary>
    public Farmer? Farmer { get; set; }

    // Auditable properties (CreatedAt, CreatedBy, ModifiedAt, ModifiedBy) are inherited from AuditableEntity
}
