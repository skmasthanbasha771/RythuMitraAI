using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Diseases.DTOs;

namespace RythuMitraAI.Application.Diseases.Queries.GetAllDiseases;

/// <summary>
/// Query to retrieve all active diseases.
/// </summary>
public sealed class GetAllDiseasesQuery : IRequest<IEnumerable<DiseaseResponse>>
{
    public GetAllDiseasesQuery()
    {
    }
}
