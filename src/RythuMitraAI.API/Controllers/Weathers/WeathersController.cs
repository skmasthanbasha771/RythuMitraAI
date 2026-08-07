using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RythuMitraAI.Application.Weathers.Commands.CreateWeather;
using RythuMitraAI.Application.Weathers.DTOs;

namespace RythuMitraAI.API.Controllers.Weathers;

[ApiController]
[Route("api/[controller]")]
public sealed class WeathersController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeathersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWeatherRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateWeatherCommand(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        // CreatedAtAction points to GetWeatherById which is not implemented yet; include id for location.
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new RythuMitraAI.Application.Weathers.Queries.GetAllWeather.GetAllWeatherQuery();
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new RythuMitraAI.Application.Weathers.Queries.GetWeatherById.GetWeatherByIdQuery(id);
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
        [FromQuery] string? weatherCode = null,
        [FromQuery] Guid? farmerId = null,
        [FromQuery] DateTime? weatherDate = null,
        [FromQuery] string? weatherCondition = null,
        [FromQuery] decimal? minTemperature = null,
        [FromQuery] decimal? maxTemperature = null,
        [FromQuery] decimal? minHumidity = null,
        [FromQuery] decimal? maxHumidity = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new RythuMitraAI.Application.Weathers.Queries.SearchWeather.SearchWeatherQuery
        {
            WeatherCode = weatherCode,
            FarmerId = farmerId,
            WeatherDate = weatherDate,
            WeatherCondition = weatherCondition,
            MinTemperature = minTemperature,
            MaxTemperature = maxTemperature,
            MinHumidity = minHumidity,
            MaxHumidity = maxHumidity,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RythuMitraAI.Application.Weathers.DTOs.UpdateWeatherRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest(new { error = "Request body is required." });

        if (id != request.Id)
            return BadRequest(new { error = "Id in route does not match request body." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new RythuMitraAI.Application.Weathers.Commands.UpdateWeather.UpdateWeatherCommand(id, request);

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
            var command = new RythuMitraAI.Application.Weathers.Commands.DeleteWeather.DeleteWeatherCommand(id);
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
