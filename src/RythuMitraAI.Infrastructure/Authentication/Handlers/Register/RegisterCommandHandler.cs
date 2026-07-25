using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Authentication.Commands.Register;
using RythuMitraAI.Application.Authentication.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;
using RythuMitraAI.Domain.Enums;
using RythuMitraAI.Infrastructure.Authentication.Services;

namespace RythuMitraAI.Infrastructure.Authentication.Handlers.Register;

/// <summary>
/// Handles user registration requests.
/// Uses repository and unit of work from the Infrastructure layer and the configured password hasher.
/// </summary>
public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<RegisterCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandHandler"/> class.
    /// </summary>
    public RegisterCommandHandler(
        IGenericRepository<User> userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ILogger<RegisterCommandHandler> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        // Check for duplicate email (case-insensitive)
        var existing = await _userRepository.FindAsync(u => u.Email.ToLower() == dto.Email.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
        {
            _logger.LogWarning("Attempt to register duplicate email {Email}", dto.Email);
            throw new InvalidOperationException("A user with the provided email already exists.");
        }

        // Hash password
        var hashed = await _passwordHasher.HashPasswordAsync(dto.Password, cancellationToken).ConfigureAwait(false);

        // Create user
        var user = new User
        {
            // Id is provided by BaseEntity default
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            Email = dto.Email.Trim(),
            PhoneNumber = dto.PhoneNumber?.Trim(),
            PasswordHash = hashed,
            Role = UserRole.Farmer,
            IsActive = true
        };

        await _userRepository.AddAsync(user, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Registered new user {UserId} with email {Email}", user.Id, user.Email);

        return new RegisterResponse
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            Message = "Registration successful."
        };
    }
}
