using System;
using MediatR;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Queries.GetFarmerById;

/// <summary>
/// Query to retrieve a single farmer by identifier.
/// </summary>
public sealed class GetFarmerByIdQuery : IRequest<FarmerResponse>
{
    /// <summary>
    /// The identifier of the farmer to retrieve.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFarmerByIdQuery"/> class.
    /// </summary>
    /// <param name="id">Farmer identifier.</param>
    public GetFarmerByIdQuery(Guid id)
    {
        Id = id;
    }
}
