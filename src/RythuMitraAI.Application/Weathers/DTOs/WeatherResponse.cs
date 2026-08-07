using System;

namespace RythuMitraAI.Application.Weathers.DTOs;

/// <summary>
/// DTO returned for weather entities.
/// </summary>
public sealed class WeatherResponse
{
    public Guid Id { get; init; }
    public string WeatherCode { get; init; } = string.Empty;
    public Guid FarmerId { get; init; }
    public DateTime WeatherDate { get; init; }
    public decimal Temperature { get; init; }
    public decimal Humidity { get; init; }
    public decimal Rainfall { get; init; }
    public decimal WindSpeed { get; init; }
    public string WeatherCondition { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
