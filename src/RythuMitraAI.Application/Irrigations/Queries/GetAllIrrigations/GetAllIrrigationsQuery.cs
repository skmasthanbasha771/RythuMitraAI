using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Irrigations.DTOs;

namespace RythuMitraAI.Application.Irrigations.Queries.GetAllIrrigations;

/// <summary>
/// Query to retrieve all active irrigations.
/// </summary>
public sealed class GetAllIrrigationsQuery : IRequest<IEnumerable<IrrigationResponse>>
{
    public GetAllIrrigationsQuery()
    {
    }
}
