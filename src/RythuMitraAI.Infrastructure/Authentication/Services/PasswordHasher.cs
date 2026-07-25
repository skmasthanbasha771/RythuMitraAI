using System.Threading;
using System.Threading.Tasks;

namespace RythuMitraAI.Infrastructure.Authentication.Services;

/// <summary>
/// BCrypt-based password hasher that performs work on a background thread to avoid blocking.
/// </summary>
public sealed class PasswordHasher : IPasswordHasher
{
    private readonly int _workFactor;

    /// <summary>
    /// Creates a new instance of <see cref="PasswordHasher"/>.
    /// </summary>
    /// <param name="workFactor">BCrypt work factor (cost). Defaults to 12. Increase for higher CPU cost.</param>
    public PasswordHasher(int workFactor = 12)
    {
        _workFactor = workFactor;
    }

    /// <inheritdoc />
    public async Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken = default)
    {
        if (password is null) throw new ArgumentNullException(nameof(password));
        cancellationToken.ThrowIfCancellationRequested();

        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            var salt = BCrypt.Net.BCrypt.GenerateSalt(_workFactor);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<bool> VerifyAsync(string hashedPassword, string providedPassword, CancellationToken cancellationToken = default)
    {
        if (hashedPassword is null) throw new ArgumentNullException(nameof(hashedPassword));
        if (providedPassword is null) throw new ArgumentNullException(nameof(providedPassword));

        cancellationToken.ThrowIfCancellationRequested();

        return await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }, cancellationToken).ConfigureAwait(false);
    }
}
