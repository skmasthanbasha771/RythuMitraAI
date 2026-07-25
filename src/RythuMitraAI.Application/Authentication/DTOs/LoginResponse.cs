using System;
using RythuMitraAI.Domain.Enums;

namespace RythuMitraAI.Application.Authentication.DTOs;

/// <summary>
/// DTO returned after successful login.
/// Contains token information and basic user details.
/// </summary>
public sealed class LoginResponse
{
    /// <summary>
    /// Access token (JWT) issued for the authenticated user.
    /// </summary>
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// Expiration date/time for the access token in UTC.
    /// </summary>
    public DateTime ExpiresAtUtc { get; init; }

    /// <summary>
    /// Identifier of the authenticated user.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Role assigned to the authenticated user.
    /// </summary>
    public UserRole Role { get; init; }
}
