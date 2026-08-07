using System;
using System.ComponentModel.DataAnnotations;

namespace RythuMitraAI.Application.Weathers.DTOs;

/// <summary>
/// DTO for updating editable weather fields.
/// </summary>
public sealed class UpdateWeatherRequest
{
    [Required]
    public Guid Id { get; init; }

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

    public bool IsActive { get; init; }
}
