using System;
using MediatR;
using RythuMitraAI.Application.Irrigations.DTOs;

namespace RythuMitraAI.Application.Irrigations.Commands.UpdateIrrigation;

/// <summary>
/// Command to update an existing irrigation.
/// </summary>
public sealed class UpdateIrrigationCommand : IRequest<IrrigationResponse>
{
    public Guid Id { get; }

    public UpdateIrrigationRequest Request { get; }

    public UpdateIrrigationCommand(Guid id, UpdateIrrigationRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
