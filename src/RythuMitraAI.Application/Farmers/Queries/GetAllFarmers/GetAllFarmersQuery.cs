using System.Collections.Generic;
using MediatR;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Queries.GetAllFarmers;

/// <summary>
/// Query to retrieve all farmers. Handled by a corresponding handler which returns a list of FarmerListResponse.
/// </summary>
public sealed class GetAllFarmersQuery : IRequest<List<FarmerListResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllFarmersQuery"/> class.
    /// </summary>
    public GetAllFarmersQuery()
    {
    }
}
