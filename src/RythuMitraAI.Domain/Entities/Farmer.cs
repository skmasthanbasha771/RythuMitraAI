using System;
using System.Collections.Generic;
using RythuMitraAI.Domain.Common;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Domain entity representing a Farmer.
/// Inherits auditing properties from <see cref="AuditableEntity"/>.
/// </summary>
public class Farmer : AuditableEntity
{
    /// <summary>
    /// Gets or sets the unique farmer code.
    /// </summary>
    public string FarmerCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the farmer's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the farmer's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the farmer's phone number. Optional.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the farmer's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the farmer's date of birth. Optional.
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the gender of the farmer.
    /// Use a string to remain persistence-agnostic; replace with an enum if available.
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the postal address of the farmer. Optional.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the village name. Optional.
    /// </summary>
    public string? Village { get; set; }

    /// <summary>
    /// Gets or sets the mandal name. Optional.
    /// </summary>
    public string? Mandal { get; set; }

    /// <summary>
    /// Gets or sets the district name. Optional.
    /// </summary>
    public string? District { get; set; }

    /// <summary>
    /// Gets or sets the state name. Optional.
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the postal code. Optional.
    /// </summary>
    public string? Pincode { get; set; }

    /// <summary>
    /// Gets or sets the land area owned by the farmer. Optional.
    /// </summary>
    /// <summary>
    /// Navigation property to the weather observations for this farmer.
    /// </summary>
    public ICollection<Weather> Weathers { get; set; } = new List<Weather>();

    public decimal? LandArea { get; set; }

    /// <summary>
    /// Gets or sets the unit used for LandArea (e.g., acres, hectares). Optional.
    /// </summary>
    public string? LandUnit { get; set; }

    /// <summary>
    /// Gets or sets the URL to the farmer's profile image. Optional.
    /// </summary>
    public string? ProfileImageUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the farmer is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets the creation time (UTC) inherited from <see cref="AuditableEntity"/>.
    /// </summary>
    public DateTime CreatedAtUtc => CreatedAt;

    /// <summary>
    /// Gets the last updated time (UTC) for the entity.
    /// Maps to the inherited <see cref="AuditableEntity.ModifiedAt"/> property.
    /// </summary>
    public DateTime? UpdatedAtUtc => ModifiedAt;
}
