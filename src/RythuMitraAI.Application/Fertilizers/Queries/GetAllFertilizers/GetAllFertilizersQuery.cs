using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.Application.Fertilizers.Queries.GetAllFertilizers;

/// <summary>
/// Query to retrieve all active fertilizers.
/// </summary>
public sealed class GetAllFertilizersQuery : IRequest<IEnumerable<FertilizerResponse>>
{
}
