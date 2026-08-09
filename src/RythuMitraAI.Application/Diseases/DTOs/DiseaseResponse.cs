using System;

namespace RythuMitraAI.Application.Diseases.DTOs;

/// <summary>
/// Response returned after creating or retrieving a disease.
/// </summary>
public sealed class DiseaseResponse
{
    public Guid Id { get; init; }

    public string DiseaseCode { get; init; } = string.Empty;

    public string DiseaseName { get; init; } = string.Empty;

    public string CropType { get; init; } = string.Empty;

    public string Symptoms { get; init; } = string.Empty;

    public string Causes { get; init; } = string.Empty;

    public string Treatment { get; init; } = string.Empty;

    public string Prevention { get; init; } = string.Empty;

    public string Severity { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public string? Message { get; init; }
}
