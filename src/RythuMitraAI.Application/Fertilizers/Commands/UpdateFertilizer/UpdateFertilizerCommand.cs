using System;
using MediatR;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.Application.Fertilizers.Commands.UpdateFertilizer;

public sealed class UpdateFertilizerCommand : IRequest<RythuMitraAI.Application.Fertilizers.DTOs.FertilizerResponse>
{
    public Guid Id { get; }
    public UpdateFertilizerRequest Request { get; }

    public UpdateFertilizerCommand(Guid id, UpdateFertilizerRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
