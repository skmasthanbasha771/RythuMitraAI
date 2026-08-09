using System;
using MediatR;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.Application.Fertilizers.Queries.GetFertilizerById;

public sealed class GetFertilizerByIdQuery : IRequest<FertilizerResponse>
{
    public Guid Id { get; }

    public GetFertilizerByIdQuery(Guid id)
    {
        Id = id;
    }
}
