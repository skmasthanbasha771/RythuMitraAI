using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RythuMitraAI.Application.Fertilizers.Commands.CreateFertilizer;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.API.Controllers.Fertilizers;

[ApiController]
[Route("api/[controller]")]
public sealed class FertilizersController : ControllerBase
{
    private readonly IMediator _mediator;

    public FertilizersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFertilizerRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateFertilizerCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new RythuMitraAI.Application.Fertilizers.Queries.GetAllFertilizers.GetAllFertilizersQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new RythuMitraAI.Application.Fertilizers.Queries.GetFertilizerById.GetFertilizerByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }
        catch (RythuMitraAI.Application.Exceptions.NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? fertilizerCode = null,
        [FromQuery] string? fertilizerName = null,
        [FromQuery] string? brand = null,
        [FromQuery] string? fertilizerType = null,
        [FromQuery] string? recommendedCrop = null,
        [FromQuery] string? recommendedSoil = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new RythuMitraAI.Application.Fertilizers.Queries.SearchFertilizers.SearchFertilizersQuery
        {
            FertilizerCode = fertilizerCode,
            FertilizerName = fertilizerName,
            Brand = brand,
            FertilizerType = fertilizerType,
            RecommendedCrop = recommendedCrop,
            RecommendedSoil = recommendedSoil,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new RythuMitraAI.Application.Fertilizers.Commands.DeleteFertilizer.DeleteFertilizerCommand(id);
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RythuMitraAI.Application.Fertilizers.DTOs.UpdateFertilizerRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (id != request.Id)
            return BadRequest(new { error = "Id in route does not match request body." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new RythuMitraAI.Application.Fertilizers.Commands.UpdateFertilizer.UpdateFertilizerCommand(id, request);

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
}
