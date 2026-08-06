using System;
using MediatR;
using RythuMitraAI.Application.Crops.DTOs;

namespace RythuMitraAI.Application.Crops.Commands.UpdateCrop;

/// <summary>
/// Command to update an existing crop.
/// </summary>
public sealed class UpdateCropCommand : IRequest<CropResponse>
{
    /// <summary>
    /// Gets the crop identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the update data.
    /// </summary>
    public UpdateCropRequest Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCropCommand"/> class.
    /// </summary>
    /// <param name="id">The crop identifier.</param>
    /// <param name="request">The update request.</param>
    public UpdateCropCommand(Guid id, UpdateCropRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
