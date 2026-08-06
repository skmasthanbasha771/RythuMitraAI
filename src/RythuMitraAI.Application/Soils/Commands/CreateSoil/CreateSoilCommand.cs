using MediatR;
using RythuMitraAI.Application.Soils.DTOs;
using System;

namespace RythuMitraAI.Application.Soils.Commands.CreateSoil;

/// <summary>
/// Command to create a soil record.
/// </summary>
public sealed class CreateSoilCommand : IRequest<SoilResponse>
{
    public CreateSoilRequest Request { get; }

    public CreateSoilCommand(CreateSoilRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
