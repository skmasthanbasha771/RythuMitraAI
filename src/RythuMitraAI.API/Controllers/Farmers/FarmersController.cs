using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Farmers.Commands.CreateFarmer;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Application.Farmers.Queries.GetAllFarmers;
using RythuMitraAI.Application.Farmers.Queries.GetFarmerById;
using RythuMitraAI.Application.Farmers.Queries.SearchFarmers;
using RythuMitraAI.Application.Farmers.Commands.UpdateFarmer;
using RythuMitraAI.Application.Farmers.Commands.DeleteFarmer;
using RythuMitraAI.Application.Exceptions;

namespace RythuMitraAI.API.Controllers.Farmers;

/// <summary>
/// Controller for farmer-related endpoints.
/// </summary>
[ApiController]
[Route("api/farmers")]
public sealed class FarmersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FarmersController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FarmersController"/> class.
    /// </summary>
    public FarmersController(IMediator mediator, ILogger<FarmersController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new farmer.
    /// </summary>
    /// <param name="request">Create farmer request DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>201 Created with created farmer data.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFarmerRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateFarmerCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        // Return 201 Created. No GET endpoint available yet for location header.
        return Created(string.Empty, result);
    }

    /// <summary>
    /// Retrieves all active farmers.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with list of farmers.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllFarmersQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a single farmer by identifier.
    /// </summary>
    /// <param name="id">Farmer identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with farmer data or 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetFarmerByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Searches farmers with paging and optional filters.
    /// </summary>
    /// <param name="pageNumber">Page number (optional, default 1).</param>
    /// <param name="pageSize">Page size (optional, default 10).</param>
    /// <param name="search">Search term applied to code, names, or phone.</param>
    /// <param name="district">Filter by district.</param>
    /// <param name="village">Filter by village.</param>
    /// <param name="state">Filter by state.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with paged search results.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? district = null,
        [FromQuery] string? village = null,
        [FromQuery] string? state = null,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchFarmersQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Search = search,
            District = district,
            Village = village,
            State = state
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Soft-deletes a farmer by identifier.
    /// </summary>
    /// <param name="id">Farmer identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>204 No Content or 404 Not Found.</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteFarmerCommand(id);
            var success = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            if (success)
                return NoContent();

            return StatusCode(500);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Updates an existing farmer.
    /// </summary>
    /// <param name="id">Farmer identifier.</param>
    /// <param name="request">Update request payload.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with updated farmer or 404 Not Found.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFarmerRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (id != request.Id)
            return BadRequest(new { error = "Id in route does not match request body." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateFarmerCommand(id)
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Address = request.Address,
                Village = request.Village,
                Mandal = request.Mandal,
                District = request.District,
                State = request.State,
                Pincode = request.Pincode,
                LandArea = request.LandArea,
                LandUnit = request.LandUnit,
                //ProfileImageUrl = request.ProfileImageUrl,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
