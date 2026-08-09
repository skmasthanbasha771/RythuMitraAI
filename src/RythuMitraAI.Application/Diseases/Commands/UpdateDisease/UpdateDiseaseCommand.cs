using System;
using MediatR;
using RythuMitraAI.Application.Diseases.DTOs;

namespace RythuMitraAI.Application.Diseases.Commands.UpdateDisease;

/// <summary>
/// Command to update an existing disease.
/// </summary>
public sealed class UpdateDiseaseCommand : IRequest<DiseaseResponse>
{
    public Guid Id { get; }

    public UpdateDiseaseRequest Request { get; }

    public UpdateDiseaseCommand(Guid id, UpdateDiseaseRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
