namespace RythuMitraAI.Application.Authentication.DTOs;

/// <summary>
/// DTO representing login request payload.
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    /// Gets or sets the email used for login.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the password used for login.
    /// </summary>
    public string Password { get; init; } = string.Empty;
}
