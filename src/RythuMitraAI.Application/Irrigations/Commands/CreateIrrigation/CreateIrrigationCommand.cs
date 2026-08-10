using MediatR;
using RythuMitraAI.Application.Irrigations.DTOs;

namespace RythuMitraAI.Application.Irrigations.Commands.CreateIrrigation;

/// <summary>
/// Command to create a new irrigation.
/// </summary>
public sealed class CreateIrrigationCommand : IRequest<IrrigationResponse>
{
    public CreateIrrigationRequest Request { get; }

    public CreateIrrigationCommand(CreateIrrigationRequest request)
    {
        Request = request ?? throw new System.ArgumentNullException(nameof(request));
    }
}
