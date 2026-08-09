using MediatR;
using RythuMitraAI.Application.Diseases.DTOs;

namespace RythuMitraAI.Application.Diseases.Commands.CreateDisease;

/// <summary>
/// Command to create a new disease.
/// </summary>
public sealed class CreateDiseaseCommand : IRequest<DiseaseResponse>
{
    public CreateDiseaseRequest Request { get; }

    public CreateDiseaseCommand(CreateDiseaseRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
