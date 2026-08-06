using System;
using System;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.Commands.CreateCrop;
using RythuMitraAI.Application.Crops.Commands.UpdateCrop;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Crops.Queries.GetAllCrops;
using RythuMitraAI.Application.Crops.Queries.GetCropById;
using RythuMitraAI.Application.Crops.Commands.DeleteCrop;
using RythuMitraAI.Application.Crops.Queries.SearchCrops;
using RythuMitraAI.Application.Exceptions;

namespace RythuMitraAI.API.Controllers.Crops;

/// <summary>
/// Controller for crop-related endpoints.
/// </summary>
[ApiController]
[Route("api/crops")]
public sealed class CropsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CropsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CropsController"/> class.
    /// </summary>
    public CropsController(IMediator mediator, ILogger<CropsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new crop.
    /// </summary>
    /// <param name="request">Create crop request DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>201 Created with crop details.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCropRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateCropCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves all active crops.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with the list of active crops.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCropsQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Searches crops with filters and pagination.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? cropName = null,
        [FromQuery] string? cropCategory = null,
        [FromQuery] string? season = null,
        [FromQuery] Guid? farmerId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchCropsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            CropName = cropName,
            CropCategory = cropCategory,
            Season = season,
            FarmerId = farmerId
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a crop by identifier.
    /// </summary>
    /// <param name="id">Crop identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with crop data or 404 Not Found.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetCropByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Updates a crop by identifier.
    /// </summary>
    /// <param name="id">Crop identifier.</param>
    /// <param name="request">Update crop request DTO.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>200 OK with updated crop data or 404 Not Found.</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCropRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateCropCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Soft-delete a crop by identifier.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteCropCommand(id);
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
}
