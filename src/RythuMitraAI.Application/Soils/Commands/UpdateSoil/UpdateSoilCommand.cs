using System;
using MediatR;
using RythuMitraAI.Application.Soils.DTOs;

namespace RythuMitraAI.Application.Soils.Commands.UpdateSoil;

/// <summary>
/// Command to update an existing soil record.
/// </summary>
public sealed class UpdateSoilCommand : IRequest<RythuMitraAI.Application.Soils.DTOs.SoilResponse>
{
    public Guid Id { get; }
    public UpdateSoilRequest Request { get; }

    public UpdateSoilCommand(Guid id, UpdateSoilRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
