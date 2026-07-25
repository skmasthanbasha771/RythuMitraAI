using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Authentication.Services;

/// <summary>
/// JWT token generator using symmetric security key and JwtSecurityTokenHandler.
/// Reads configuration values from the "JwtSettings" configuration section.
/// </summary>
public sealed class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtTokenService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenService"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration to read JwtSettings.</param>
    /// <param name="logger">Logger instance.</param>
    public JwtTokenService(IConfiguration configuration, ILogger<JwtTokenService> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public Task<(string Token, DateTime ExpiresAtUtc)> GenerateTokenAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        cancellationToken.ThrowIfCancellationRequested();

        var section = _configuration.GetSection("JwtSettings");
        var secret = section.GetValue<string>("Secret");
        var issuer = section.GetValue<string>("Issuer");
        var audience = section.GetValue<string>("Audience");
        var expirationMinutes = section.GetValue<int?>("ExpirationMinutes") ?? 60;

        if (string.IsNullOrWhiteSpace(secret))
        {
            _logger.LogError("JWT secret is not configured. Configure JwtSettings:Secret in configuration.");
            throw new InvalidOperationException("JWT secret not configured.");
        }

        var keyBytes = Encoding.UTF8.GetBytes(secret);
        var securityKey = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(expirationMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(tokenDescriptor);

        return Task.FromResult((token, expires));
    }
}
