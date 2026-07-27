using System;
using MediatR;
using RythuMitraAI.Application.Farmers.DTOs;

namespace RythuMitraAI.Application.Farmers.Commands.CreateFarmer;

/// <summary>
/// Command to create a new Farmer.
/// The command carries the data required to create the entity and is handled by a corresponding handler.
/// </summary>
public sealed class CreateFarmerCommand : IRequest<CreateFarmerResponse>
{
    /// <summary>
    /// The request DTO containing farmer creation data.
    /// </summary>
    public CreateFarmerRequest Request { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFarmerCommand"/> class.
    /// </summary>
    /// <param name="request">The create farmer request.</param>
    public CreateFarmerCommand(CreateFarmerRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
