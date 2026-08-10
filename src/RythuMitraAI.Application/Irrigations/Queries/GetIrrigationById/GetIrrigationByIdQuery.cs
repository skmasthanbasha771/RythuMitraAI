using System;
using MediatR;
using RythuMitraAI.Application.Irrigations.DTOs;

namespace RythuMitraAI.Application.Irrigations.Queries.GetIrrigationById;

/// <summary>
/// Query to retrieve an irrigation record by identifier.
/// </summary>
public sealed class GetIrrigationByIdQuery : IRequest<IrrigationResponse>
{
    public Guid Id { get; }

    public GetIrrigationByIdQuery(Guid id)
    {
        Id = id;
    }
}
