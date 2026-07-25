using RythuMitraAI.Domain.Common;
using RythuMitraAI.Domain.Enums;

namespace RythuMitraAI.Domain.Entities;

/// <summary>
/// Represents an application user.
/// Inherits audit information from <see cref="AuditableEntity"/>.
/// </summary>
public class User : AuditableEntity
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's phone number. This is optional and may be <c>null</c>.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the hashed password for the user.
    /// Store only the password hash; do not store plaintext passwords.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
