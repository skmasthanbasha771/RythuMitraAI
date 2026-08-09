using System;
using MediatR;
using RythuMitraAI.Application.Diseases.DTOs;

namespace RythuMitraAI.Application.Diseases.Queries.GetDiseaseById;

/// <summary>
/// Query to retrieve a disease by identifier.
/// </summary>
public sealed class GetDiseaseByIdQuery : IRequest<DiseaseResponse>
{
    public Guid Id { get; }

    public GetDiseaseByIdQuery(Guid id)
    {
        Id = id;
    }
}
