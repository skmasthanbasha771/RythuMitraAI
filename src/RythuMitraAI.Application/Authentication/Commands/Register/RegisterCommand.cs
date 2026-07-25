using MediatR;
using RythuMitraAI.Application.Authentication.DTOs;

namespace RythuMitraAI.Application.Authentication.Commands.Register;

/// <summary>
/// Command to register a new user.
/// Handled by a corresponding command handler which performs validation, hashing and persistence.
/// </summary>
public sealed class RegisterCommand : IRequest<RegisterResponse>
{
    /// <summary>
    /// Gets the registration request data.
    /// </summary>
    public RegisterRequest Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommand"/> class.
    /// </summary>
    /// <param name="request">The registration request DTO.</param>
    public RegisterCommand(RegisterRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
