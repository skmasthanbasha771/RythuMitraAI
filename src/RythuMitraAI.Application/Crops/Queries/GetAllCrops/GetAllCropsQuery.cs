using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Crops.DTOs;

namespace RythuMitraAI.Application.Crops.Queries.GetAllCrops;

/// <summary>
/// Query to retrieve all active crops.
/// </summary>
public sealed class GetAllCropsQuery : IRequest<IEnumerable<CropResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCropsQuery"/> class.
    /// </summary>
    public GetAllCropsQuery()
    {
    }
}
