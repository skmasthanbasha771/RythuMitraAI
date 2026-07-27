using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Farmers.DTOs;

/// <summary>
/// DTO used to create a new farmer.
/// </summary>
public sealed class CreateFarmerRequest
{
    /// <summary>
    /// Unique farmer code provided by the system or client.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string FarmerCode { get; init; } = string.Empty;

    /// <summary>
    /// First name of the farmer.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Last name of the farmer.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Phone number of the farmer.
    /// </summary>
    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; init; } = string.Empty;

    /// <summary>
    /// Email address of the farmer.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Date of birth (optional).
    /// </summary>
    public DateTime? DateOfBirth { get; init; }

    /// <summary>
    /// Gender (optional).
    /// </summary>
    [MaxLength(50)]
    public string? Gender { get; init; }

    /// <summary>
    /// Postal address (optional).
    /// </summary>
    [MaxLength(500)]
    public string? Address { get; init; }

    /// <summary>
    /// Village (optional).
    /// </summary>
    [MaxLength(150)]
    public string? Village { get; init; }

    /// <summary>
    /// Mandal (optional).
    /// </summary>
    [MaxLength(150)]
    public string? Mandal { get; init; }

    /// <summary>
    /// District (optional).
    /// </summary>
    [MaxLength(150)]
    public string? District { get; init; }

    /// <summary>
    /// State (optional).
    /// </summary>
    [MaxLength(150)]
    public string? State { get; init; }

    /// <summary>
    /// Postal code (optional).
    /// </summary>
    [MaxLength(20)]
    public string? Pincode { get; init; }

    /// <summary>
    /// Land area (optional).
    /// </summary>
    [Range(0, Double.MaxValue)]
    public decimal? LandArea { get; init; }

    /// <summary>
    /// Unit for land area (e.g., acres, hectares).
    /// </summary>
    [MaxLength(50)]
    public string? LandUnit { get; init; }

    /// <summary>
    /// URL to profile image (optional).
    /// </summary>
    //[Url]
    //[MaxLength(2048)]
    //public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// Indicates whether the farmer is active. Defaults to true.
    /// </summary>
    public bool IsActive { get; init; } = true;
}
