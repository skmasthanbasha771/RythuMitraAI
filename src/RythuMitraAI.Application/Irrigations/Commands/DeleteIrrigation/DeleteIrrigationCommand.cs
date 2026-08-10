using System;
using MediatR;

namespace RythuMitraAI.Application.Irrigations.Commands.DeleteIrrigation;

/// <summary>
/// Command to soft delete an irrigation by identifier.
/// </summary>
public sealed class DeleteIrrigationCommand : IRequest<bool>
{
    /// <summary>
    /// The identifier of the irrigation to delete.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteIrrigationCommand"/> class.
    /// </summary>
    /// <param name="id">The irrigation identifier.</param>
    public DeleteIrrigationCommand(Guid id)
    {
        Id = id;
    }
}
