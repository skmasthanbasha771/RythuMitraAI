using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RythuMitraAI.API.Models.Requests;

/// <summary>
/// API request model for uploading a farmer's profile image.
/// </summary>
public sealed class UploadFarmerProfileImageRequest
{
    /// <summary>
    /// The image file to upload.
    /// </summary>
    [Required]
    public IFormFile Image { get; init; } = default!;
}
