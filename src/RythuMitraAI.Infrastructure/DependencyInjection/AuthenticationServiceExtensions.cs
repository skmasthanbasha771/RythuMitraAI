using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RythuMitraAI.Infrastructure.Authentication.Services;

namespace RythuMitraAI.Infrastructure.DependencyInjection;

/// <summary>
/// Registers infrastructure authentication services.
/// This extension intentionally does not configure ASP.NET Core authentication/authorization
/// and should only register infrastructure implementations that do not depend on web host packages.
/// </summary>
public static class AuthenticationServiceExtensions
{
    /// <summary>
    /// Adds infrastructure authentication services (password hasher and JWT token service).
    /// </summary>
    public static IServiceCollection AddInfrastructureAuthentication(this IServiceCollection services, IConfiguration _configuration)
    {
        // Register infrastructure implementations only. Do NOT configure ASP.NET Core middleware here.
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
