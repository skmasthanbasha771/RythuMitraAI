using System;
using MediatR;
using RythuMitraAI.Application.Crops.DTOs;

namespace RythuMitraAI.Application.Crops.Queries.GetCropById;

/// <summary>
/// Query to retrieve a crop by its identifier.
/// </summary>
public sealed class GetCropByIdQuery : IRequest<CropResponse>
{
    /// <summary>
    /// Gets the crop identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCropByIdQuery"/> class.
    /// </summary>
    /// <param name="id">The crop identifier.</param>
    public GetCropByIdQuery(Guid id)
    {
        Id = id;
    }
}
