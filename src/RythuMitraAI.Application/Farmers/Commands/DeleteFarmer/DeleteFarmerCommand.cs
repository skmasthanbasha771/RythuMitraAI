using System;
using MediatR;

namespace RythuMitraAI.Application.Farmers.Commands.DeleteFarmer;

/// <summary>
/// Command to delete a farmer by identifier.
/// </summary>
public sealed class DeleteFarmerCommand : IRequest<bool>
{
    /// <summary>
    /// The identifier of the farmer to delete.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFarmerCommand"/> class.
    /// </summary>
    /// <param name="id">The farmer identifier.</param>
    public DeleteFarmerCommand(Guid id)
    {
        Id = id;
    }
}
