using MediatR;
using RythuMitraAI.Application.Authentication.DTOs;

namespace RythuMitraAI.Application.Authentication.Commands.Login;

/// <summary>
/// Command to authenticate a user and obtain a JWT token.
/// </summary>
public sealed class LoginCommand : IRequest<LoginResponse>
{
    /// <summary>
    /// Gets the login request payload.
    /// </summary>
    public LoginRequest Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommand"/> class.
    /// </summary>
    /// <param name="request">The login request DTO.</param>
    public LoginCommand(LoginRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
