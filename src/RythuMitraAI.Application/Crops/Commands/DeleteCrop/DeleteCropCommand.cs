using System;
using MediatR;

namespace RythuMitraAI.Application.Crops.Commands.DeleteCrop;

/// <summary>
/// Command to soft delete a crop by identifier.
/// </summary>
public sealed class DeleteCropCommand : IRequest<bool>
{
    /// <summary>
    /// The identifier of the crop to delete.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCropCommand"/> class.
    /// </summary>
    /// <param name="id">The crop identifier.</param>
    public DeleteCropCommand(Guid id)
    {
        Id = id;
    }
}
