using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Authentication.Commands.Login;
using RythuMitraAI.Application.Authentication.Commands.Register;
using RythuMitraAI.Application.Authentication.DTOs;

namespace RythuMitraAI.API.Controllers;

/// <summary>
/// Authentication endpoints for registering and logging in users.
/// Controllers remain thin and delegate work to MediatR handlers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">Registration request payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Registration response with created user details.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new RegisterCommand(request);
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            // Return 201 Created with the response body. Location header omitted (no GET endpoint available).
            return Created(string.Empty, result);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Registration conflict for email {Email}", request.Email);
            return Conflict(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Authenticates a user and returns a JWT access token.
    /// </summary>
    /// <param name="request">Login request payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Login response containing access token and metadata.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new LoginCommand(request);
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Failed login attempt for email {Email}", request.Email);
            return Unauthorized(new { error = "Invalid credentials." });
        }
    }
}
