using System;

namespace RythuMitraAI.Application.Authentication.DTOs;

/// <summary>
/// DTO for user registration request.
/// Contains the information required to create a new user account.
/// </summary>
public sealed class RegisterRequest
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's phone number. Optional.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets or sets the password for the new account.
    /// Plain text only for transfer; hashing must be performed before persistence.
    /// </summary>
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the password confirmation. Must match <see cref="Password"/>.
    /// </summary>
    public string ConfirmPassword { get; init; } = string.Empty;
}
