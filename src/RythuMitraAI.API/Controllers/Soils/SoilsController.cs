using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RythuMitraAI.Application.Soils.Commands.CreateSoil;
using RythuMitraAI.Application.Soils.DTOs;

namespace RythuMitraAI.API.Controllers.Soils;

[ApiController]
[Route("api/[controller]")]
public sealed class SoilsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SoilsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? soilCode = null,
        [FromQuery] Guid? farmerId = null,
        [FromQuery] DateTime? testDate = null,
        [FromQuery] decimal? minPH = null,
        [FromQuery] decimal? maxPH = null,
        [FromQuery] decimal? minNitrogen = null,
        [FromQuery] decimal? maxNitrogen = null,
        [FromQuery] decimal? minPhosphorus = null,
        [FromQuery] decimal? maxPhosphorus = null,
        [FromQuery] decimal? minPotassium = null,
        [FromQuery] decimal? maxPotassium = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new RythuMitraAI.Application.Soils.Queries.SearchSoils.SearchSoilsQuery
        {
            SoilCode = soilCode,
            FarmerId = farmerId,
            TestDate = testDate,
            MinPH = minPH,
            MaxPH = maxPH,
            MinNitrogen = minNitrogen,
            MaxNitrogen = maxNitrogen,
            MinPhosphorus = minPhosphorus,
            MaxPhosphorus = maxPhosphorus,
            MinPotassium = minPotassium,
            MaxPotassium = maxPotassium,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSoilRequest request)
    {
        if (request is null)
            return BadRequest("Request body is required.");

        var command = new CreateSoilCommand(request);
        var result = await _mediator.Send(command).ConfigureAwait(false);

        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new RythuMitraAI.Application.Soils.Queries.GetAllSoils.GetAllSoilsQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new RythuMitraAI.Application.Soils.Queries.GetSoilById.GetSoilByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RythuMitraAI.Application.Soils.DTOs.UpdateSoilRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (id != request.Id)
            return BadRequest(new { error = "Id in route does not match request body." });

        var command = new RythuMitraAI.Application.Soils.Commands.UpdateSoil.UpdateSoilCommand(id, request);

        try
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new RythuMitraAI.Application.Soils.Commands.DeleteSoil.DeleteSoilCommand(id);
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
}
