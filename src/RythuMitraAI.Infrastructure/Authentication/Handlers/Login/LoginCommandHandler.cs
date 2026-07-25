using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Authentication.Commands.Login;
using RythuMitraAI.Application.Authentication.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;
using RythuMitraAI.Infrastructure.Authentication.Services;

namespace RythuMitraAI.Infrastructure.Authentication.Handlers.Login;

/// <summary>
/// Handles user login requests: verifies credentials and issues JWT tokens.
/// </summary>
public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandHandler"/> class.
    /// </summary>
    public LoginCommandHandler(
        IGenericRepository<User> userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        var email = dto.Email?.Trim() ?? string.Empty;

        var users = await _userRepository.FindAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken).ConfigureAwait(false);
        var user = users.FirstOrDefault();

        if (user is null)
        {
            _logger.LogWarning("Login attempt failed: user not found for email {Email}", email);
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var passwordValid = await _passwordHasher.VerifyAsync(user.PasswordHash, dto.Password, cancellationToken).ConfigureAwait(false);
        if (!passwordValid)
        {
            _logger.LogWarning("Login attempt failed: invalid password for user {UserId}", user.Id);
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Generate JWT token and expiry
        var (token, expiresAt) = await _jwtTokenService.GenerateTokenAsync(user, cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("User {UserId} logged in", user.Id);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresAtUtc = expiresAt,
            UserId = user.Id,
            Role = user.Role
        };
    }
}
