using System;

namespace RythuMitraAI.Application.Farmers.DTOs;

/// <summary>
/// Response returned after creating a farmer.
/// </summary>
public sealed class CreateFarmerResponse
{
    /// <summary>
    /// Identifier of the created farmer.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Farmer code assigned to the created farmer.
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
    /// Email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Indicates whether the farmer is active.
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
