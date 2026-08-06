using System;
using MediatR;

namespace RythuMitraAI.Application.Soils.Commands.DeleteSoil;

/// <summary>
/// Command to soft-delete a soil by id.
/// </summary>
public sealed class DeleteSoilCommand : IRequest<bool>
{
    public Guid Id { get; }

    public DeleteSoilCommand(Guid id)
    {
        Id = id;
    }
}
