namespace RythuMitraAI.Domain.Enums;

/// <summary>
/// Defines application user roles used for authorization and role-based behavior.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// Administrative user with full permissions.
    /// </summary>
    Admin,

    /// <summary>
    /// Agricultural producer (farmer) who can manage crops, requests and related data.
    /// </summary>
    Farmer,

    /// <summary>
    /// Buyer who can browse and purchase agricultural produce.
    /// </summary>
    Buyer
}
