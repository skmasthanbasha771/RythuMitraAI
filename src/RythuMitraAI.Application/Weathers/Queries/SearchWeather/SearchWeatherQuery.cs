using System;
using MediatR;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Weathers.DTOs;

namespace RythuMitraAI.Application.Weathers.Queries.SearchWeather;

/// <summary>
/// Query to search weather records with filters and pagination.
/// </summary>
public sealed class SearchWeatherQuery : IRequest<PagedResponse<WeatherResponse>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public string? WeatherCode { get; init; }
    public Guid? FarmerId { get; init; }
    public DateTime? WeatherDate { get; init; }
    public string? WeatherCondition { get; init; }

    public decimal? MinTemperature { get; init; }
    public decimal? MaxTemperature { get; init; }

    public decimal? MinHumidity { get; init; }
    public decimal? MaxHumidity { get; init; }
}
