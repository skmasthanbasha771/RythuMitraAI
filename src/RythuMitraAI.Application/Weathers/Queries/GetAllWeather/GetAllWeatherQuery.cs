using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Weathers.DTOs;

namespace RythuMitraAI.Application.Weathers.Queries.GetAllWeather;

/// <summary>
/// Query to retrieve all active weather records.
/// </summary>
public sealed class GetAllWeatherQuery : IRequest<IEnumerable<WeatherResponse>>
{
}
