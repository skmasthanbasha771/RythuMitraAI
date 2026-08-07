using System;
using MediatR;
using RythuMitraAI.Application.Weathers.DTOs;

namespace RythuMitraAI.Application.Weathers.Queries.GetWeatherById;

/// <summary>
/// Query to retrieve a single weather record by id.
/// </summary>
public sealed class GetWeatherByIdQuery : IRequest<WeatherResponse>
{
    public Guid Id { get; }

    public GetWeatherByIdQuery(Guid id)
    {
        Id = id;
    }
}
