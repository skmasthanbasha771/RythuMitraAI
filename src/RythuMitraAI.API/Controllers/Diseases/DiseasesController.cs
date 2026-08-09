using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.Commands.CreateDisease;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Diseases.Queries.GetAllDiseases;
using RythuMitraAI.Application.Diseases.Queries.GetDiseaseById;
using RythuMitraAI.Application.Diseases.Commands.UpdateDisease;
using RythuMitraAI.Application.Diseases.Commands.UpdateDisease;
using RythuMitraAI.Application.Diseases.Commands.DeleteDisease;
using RythuMitraAI.Application.Diseases.Queries.SearchDiseases;

using System.Collections.Generic;

namespace RythuMitraAI.API.Controllers.Diseases;

/// <summary>
/// Controller for disease-related endpoints.
/// </summary>
[ApiController]
[Route("api/diseases")]
public sealed class DiseasesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DiseasesController> _logger;

    public DiseasesController(IMediator mediator, ILogger<DiseasesController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new disease record.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiseaseRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateDiseaseCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        // Return 201 Created. No GET endpoint implemented; include id for location.
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves all active diseases.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllDiseasesQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a disease by identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetDiseaseByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Searches diseases with optional filters and pagination.
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? diseaseCode = null,
        [FromQuery] string? diseaseName = null,
        [FromQuery] string? cropType = null,
        [FromQuery] string? severity = null,
        [FromQuery] bool? isActive = null,
        CancellationToken cancellationToken = default)
    {
        var query = new SearchDiseasesQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            DiseaseCode = diseaseCode,
            DiseaseName = diseaseName,
            CropType = cropType,
            Severity = severity,
            IsActive = isActive
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    /// <summary>
    /// Soft-deletes a disease by identifier.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteDiseaseCommand(id);
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
    /// Updates an existing disease.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDiseaseRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = new UpdateDiseaseCommand(id, request);
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }
}
