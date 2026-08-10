using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.Commands.CreateIrrigation;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Irrigations.Queries.GetAllIrrigations;
using RythuMitraAI.Application.Irrigations.Queries.GetIrrigationById;
using RythuMitraAI.Application.Irrigations.Commands.UpdateIrrigation;
using RythuMitraAI.Application.Irrigations.Commands.DeleteIrrigation;
using RythuMitraAI.Application.Irrigations.Queries.SearchIrrigations;
using System;
using System.Collections.Generic;

namespace RythuMitraAI.API.Controllers.Irrigations;

/// <summary>
/// Controller for irrigation-related endpoints.
/// </summary>
[ApiController]
[Route("api/irrigations")]
public sealed class IrrigationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<IrrigationsController> _logger;

    public IrrigationsController(IMediator mediator, ILogger<IrrigationsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new irrigation record.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIrrigationRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateIrrigationCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // Note: GetIrrigationById is not implemented yet. CreatedAtAction references it for the Location header.

    /// <summary>
    /// Retrieves all active irrigations.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllIrrigationsQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves an irrigation by identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetIrrigationByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Searches irrigations with optional filters and pagination.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? irrigationCode = null,
        [FromQuery] Guid? farmerId = null,
        [FromQuery] Guid? cropId = null,
        [FromQuery] string? irrigationType = null,
        [FromQuery] string? waterSource = null,
        [FromQuery] DateTime? irrigationDate = null,
        [FromQuery] bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchIrrigationsQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            IrrigationCode = irrigationCode,
            FarmerId = farmerId,
            CropId = cropId,
            IrrigationType = irrigationType,
            WaterSource = waterSource,
            IrrigationDate = irrigationDate,
            IsActive = isActive
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Soft-deletes an irrigation by identifier.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteIrrigationCommand(id);
            var success = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            if (success)
                return NoContent();

            return StatusCode(500);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Updates an existing irrigation.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIrrigationRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateIrrigationCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }
}
