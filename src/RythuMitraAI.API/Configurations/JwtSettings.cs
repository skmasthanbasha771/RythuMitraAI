namespace RythuMitraAI.API.Configurations;

/// <summary>
/// Represents JWT configuration settings bound from configuration (appsettings.json).
/// </summary>
public sealed class JwtSettings
{
    /// <summary>
    /// The secret key used to sign JWT tokens. Must be a sufficiently long random value.
    /// </summary>
    public string Secret { get; init; } = string.Empty;

    /// <summary>
    /// The issuer (iss) claim to set on generated tokens.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// The audience (aud) claim to set on generated tokens.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// Token expiration in minutes.
    /// </summary>
    public int ExpirationMinutes { get; init; } = 60;
}
