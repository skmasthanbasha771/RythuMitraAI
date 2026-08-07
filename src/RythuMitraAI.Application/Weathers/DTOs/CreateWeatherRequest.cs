using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Weathers.DTOs;

/// <summary>
/// DTO for creating a new weather observation.
/// </summary>
public sealed class CreateWeatherRequest
{
    [Required]
    [MaxLength(50)]
    public string WeatherCode { get; init; } = string.Empty;

    [Required]
    public Guid FarmerId { get; init; }

    [Required]
    public DateTime WeatherDate { get; init; }

    [Required]
    public decimal Temperature { get; init; }

    [Required]
    public decimal Humidity { get; init; }

    [Required]
    public decimal Rainfall { get; init; }

    [Required]
    public decimal WindSpeed { get; init; }

    [Required]
    [MaxLength(100)]
    public string WeatherCondition { get; init; } = string.Empty;

    public bool IsActive { get; init; } = true;
}
