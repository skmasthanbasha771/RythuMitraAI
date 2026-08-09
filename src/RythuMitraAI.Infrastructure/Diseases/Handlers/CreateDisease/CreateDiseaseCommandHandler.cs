using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.Commands.CreateDisease;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.CreateDisease;

/// <summary>
/// Handles <see cref="CreateDiseaseCommand"/> to create and persist a Disease entity.
/// </summary>
public sealed class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommand, DiseaseResponse>
{
    private readonly IGenericRepository<Disease> _diseaseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateDiseaseCommandHandler> _logger;

    public CreateDiseaseCommandHandler(
        IGenericRepository<Disease> diseaseRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateDiseaseCommandHandler> logger)
    {
        _diseaseRepository = diseaseRepository ?? throw new ArgumentNullException(nameof(diseaseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DiseaseResponse> Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        var existing = await _diseaseRepository.FindAsync(d => d.DiseaseCode.ToLower() == dto.DiseaseCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
            throw new InvalidOperationException($"DiseaseCode '{dto.DiseaseCode}' already exists.");

        var disease = new Disease
        {
            DiseaseCode = dto.DiseaseCode.Trim(),
            DiseaseName = dto.DiseaseName.Trim(),
            CropType = dto.CropType.Trim(),
            Symptoms = dto.Symptoms.Trim(),
            Causes = dto.Causes.Trim(),
            Treatment = dto.Treatment.Trim(),
            Prevention = dto.Prevention.Trim(),
            Severity = dto.Severity.Trim(),
            IsActive = dto.IsActive
        };

        disease.SetCreated();

        await _diseaseRepository.AddAsync(disease, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created disease {DiseaseId} with code {DiseaseCode}", disease.Id, disease.DiseaseCode);

        return new DiseaseResponse
        {
            Id = disease.Id,
            DiseaseCode = disease.DiseaseCode,
            DiseaseName = disease.DiseaseName,
            CropType = disease.CropType,
            Symptoms = disease.Symptoms,
            Causes = disease.Causes,
            Treatment = disease.Treatment,
            Prevention = disease.Prevention,
            Severity = disease.Severity,
            IsActive = disease.IsActive,
            CreatedAtUtc = disease.CreatedAt,
            Message = "Disease created successfully."
        };
    }
}
