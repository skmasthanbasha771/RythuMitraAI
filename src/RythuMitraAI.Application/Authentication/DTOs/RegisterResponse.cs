using System;
using RythuMitraAI.Domain.Enums;

namespace RythuMitraAI.Application.Authentication.DTOs;

/// <summary>
/// DTO returned after a successful registration.
/// </summary>
public sealed class RegisterResponse
{
    /// <summary>
    /// Gets the identifier of the newly created user.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the email of the newly created user.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets the role assigned to the user.
    /// </summary>
    public UserRole Role { get; init; }

    /// <summary>
    /// Gets a value indicating whether the user is active.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Optional message for the client (e.g., informational or success message).
    /// </summary>
    public string? Message { get; init; }
}
