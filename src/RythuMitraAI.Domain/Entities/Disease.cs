using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Domain entity representing an agricultural disease or pest information.
/// Inherits auditing properties from <see cref="AuditableEntity"/>.
/// </summary>
public class Disease : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique disease code.
    /// </summary>
    public string DiseaseCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the human-friendly disease name.
    /// </summary>
    public string DiseaseName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the crop type affected by the disease.
    /// </summary>
    public string CropType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the symptoms associated with the disease.
    /// </summary>
    public string Symptoms { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the known causes of the disease.
    /// </summary>
    public string Causes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets recommended treatment information for the disease.
    /// </summary>
    public string Treatment { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets prevention and management practices for the disease.
    /// </summary>
    public string Prevention { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the severity level of the disease (e.g., Low, Medium, High).
    /// </summary>
    public string Severity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the disease record is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
