using System.Threading;
using System.Threading.Tasks;

namespace RythuMitraAI.Infrastructure.Authentication.Services;

/// <summary>
/// Abstraction for password hashing and verification.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Computes a secure password hash for the provided plain-text password.
    /// Implementations should use a proven algorithm such as BCrypt and
    /// should be safe to call from background threads.
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The hashed password as a string.</returns>
    Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies whether the provided plain-text password matches the stored hash.
    /// </summary>
    /// <param name="hashedPassword">The stored password hash.</param>
    /// <param name="providedPassword">The plain-text password to verify.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the password matches; otherwise false.</returns>
    Task<bool> VerifyAsync(string hashedPassword, string providedPassword, CancellationToken cancellationToken = default);
}
