using System;
using System.Threading;
using System.Threading.Tasks;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Authentication.Services;

/// <summary>
/// Abstraction for generating JWT tokens for authenticated users.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a signed JWT access token for the specified user.
    /// Returns the token string and its UTC expiration time.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Tuple of token string and expiration <see cref="DateTime"/> (UTC).</returns>
    Task<(string Token, DateTime ExpiresAtUtc)> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
}
