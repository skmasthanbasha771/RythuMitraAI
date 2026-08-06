using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Soils.DTOs;

namespace RythuMitraAI.Application.Soils.Queries.GetAllSoils;

/// <summary>
/// Query to retrieve all active soils.
/// </summary>
public sealed class GetAllSoilsQuery : IRequest<IEnumerable<SoilResponse>>
{
}
