using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Farmers.DTOs;

/// <summary>
/// DTO representing a farmer returned by queries.
/// </summary>
public sealed class FarmerResponse
{
    /// <summary>
    /// Gets the farmer identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Unique farmer code.
    /// </summary>
    public string FarmerCode { get; init; } = string.Empty;

    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Date of birth (optional).
    /// </summary>
    public DateTime? DateOfBirth { get; init; }

    /// <summary>
    /// Gender (optional).
    /// </summary>
    public string? Gender { get; init; }

    /// <summary>
    /// Address (optional).
    /// </summary>
    public string? Address { get; init; }

    /// <summary>
    /// Village (optional).
    /// </summary>
    public string? Village { get; init; }

    /// <summary>
    /// Mandal (optional).
    /// </summary>
    public string? Mandal { get; init; }

    /// <summary>
    /// District (optional).
    /// </summary>
    public string? District { get; init; }

    /// <summary>
    /// State (optional).
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// Postal code (optional).
    /// </summary>
    public string? Pincode { get; init; }

    /// <summary>
    /// Land area (optional).
    /// </summary>
    public decimal? LandArea { get; init; }

    /// <summary>
    /// Land unit (optional).
    /// </summary>
    public string? LandUnit { get; init; }

    /// <summary>
    /// Profile image URL (optional).
    /// </summary>
    public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// Indicates whether the farmer is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Creation time UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; init; }

    /// <summary>
    /// Last modified time UTC.
    /// </summary>
    public DateTime? UpdatedAtUtc { get; init; }
}
