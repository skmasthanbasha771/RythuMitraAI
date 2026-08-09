using System;
using MediatR;

namespace RythuMitraAI.Application.Diseases.Commands.DeleteDisease;

/// <summary>
/// Command to soft delete a disease by identifier.
/// </summary>
public sealed class DeleteDiseaseCommand : IRequest<bool>
{
    /// <summary>
    /// The identifier of the disease to delete.
    /// </summary>
    public Guid Id { get; }

    public DeleteDiseaseCommand(Guid id)
    {
        Id = id;
    }
}
