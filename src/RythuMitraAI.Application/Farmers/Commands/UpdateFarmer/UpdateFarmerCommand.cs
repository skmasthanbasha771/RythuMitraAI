using System;
using MediatR;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Commands.UpdateFarmer;

/// <summary>
/// Command to update an existing farmer.
/// Handled by a corresponding handler which performs validation and persistence.
/// </summary>
public sealed class UpdateFarmerCommand : IRequest<FarmerResponse>
{
    /// <summary>
    /// Identifier of the farmer to update.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// First name of the farmer.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Last name of the farmer.
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Phone number of the farmer.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Email address of the farmer.
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
    /// Postal address (optional).
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
    /// Unit for land area (e.g., acres, hectares).
    /// </summary>
    public string? LandUnit { get; init; }

    /// <summary>
    /// URL to profile image (optional).
    /// </summary>
    public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// Indicates whether the farmer is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFarmerCommand"/> class.
    /// </summary>
    /// <param name="id">The identifier of the farmer to update.</param>
    public UpdateFarmerCommand(Guid id)
    {
        Id = id;
    }
}
