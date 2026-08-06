using System;
using MediatR;
using RythuMitraAI.Application.Crops.DTOs;

namespace RythuMitraAI.Application.Crops.Commands.CreateCrop;

/// <summary>
/// Command to create a new crop.
/// </summary>
public sealed class CreateCropCommand : IRequest<CropResponse>
{
    /// <summary>
    /// The request DTO containing crop creation data.
    /// </summary>
    public CreateCropRequest Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCropCommand"/> class.
    /// </summary>
    /// <param name="request">The create crop request.</param>
    public CreateCropCommand(CreateCropRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
