using System;
using MediatR;

namespace RythuMitraAI.Application.Fertilizers.Commands.DeleteFertilizer;

public sealed class DeleteFertilizerCommand : IRequest<bool>
{
    public Guid Id { get; }

    public DeleteFertilizerCommand(Guid id)
    {
        Id = id;
    }
}
