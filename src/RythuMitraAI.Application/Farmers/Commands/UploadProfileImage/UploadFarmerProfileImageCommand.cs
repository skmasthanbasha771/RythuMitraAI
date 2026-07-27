using System;
using System.IO;
using MediatR;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Commands.UploadProfileImage;

/// <summary>
/// Command to upload a farmer's profile image. Handled by an infrastructure handler that
/// receives the image stream and persists it to storage, updating the farmer entity.
/// </summary>
public sealed class UploadFarmerProfileImageCommand : IRequest<FarmerResponse>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UploadFarmerProfileImageCommand"/> class.
    /// </summary>
    /// <param name="farmerId">Identifier of the farmer.</param>
    /// <param name="imageStream">Stream containing the image content. Caller retains ownership of the stream.</param>
    /// <param name="fileName">Original file name of the uploaded image.</param>
    /// <param name="contentType">MIME content type of the image.</param>
    public UploadFarmerProfileImageCommand(Guid farmerId, Stream imageStream, string fileName, string contentType)
    {
        FarmerId = farmerId;
        ImageStream = imageStream ?? throw new ArgumentNullException(nameof(imageStream));
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
        ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
    }

    /// <summary>
    /// Gets the farmer identifier.
    /// </summary>
    public Guid FarmerId { get; }

    /// <summary>
    /// Gets the image stream. The handler should not dispose the stream if the owner expects to reuse it.
    /// </summary>
    public Stream ImageStream { get; }

    /// <summary>
    /// Gets the original file name of the image.
    /// </summary>
    public string FileName { get; }

    /// <summary>
    /// Gets the MIME content type of the image (e.g., image/png).
    /// </summary>
    public string ContentType { get; }
}
