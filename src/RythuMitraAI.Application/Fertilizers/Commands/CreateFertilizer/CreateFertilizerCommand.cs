using MediatR;
using RythuMitraAI.Application.Fertilizers.DTOs;

namespace RythuMitraAI.Application.Fertilizers.Commands.CreateFertilizer;

public sealed class CreateFertilizerCommand : IRequest<FertilizerResponse>
{
    public CreateFertilizerRequest Request { get; }

    public CreateFertilizerCommand(CreateFertilizerRequest request)
    {
        Request = request ?? throw new System.ArgumentNullException(nameof(request));
    }
}
