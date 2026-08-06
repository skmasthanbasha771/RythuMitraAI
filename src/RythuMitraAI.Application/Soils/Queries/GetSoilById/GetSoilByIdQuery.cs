using System;
using MediatR;
using RythuMitraAI.Application.Soils.DTOs;

namespace RythuMitraAI.Application.Soils.Queries.GetSoilById;

/// <summary>
/// Query to retrieve a soil by id.
/// </summary>
public sealed class GetSoilByIdQuery : IRequest<SoilResponse>
{
    public Guid Id { get; }

    public GetSoilByIdQuery(Guid id)
    {
        Id = id;
    }
}
