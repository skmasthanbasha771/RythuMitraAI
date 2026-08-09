using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.Commands.UpdateDisease;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.UpdateDisease;

/// <summary>
/// Handles updating an existing disease.
/// </summary>
public sealed class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommand, DiseaseResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateDiseaseCommandHandler> _logger;

    public UpdateDiseaseCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateDiseaseCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task<DiseaseResponse> Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new System.ArgumentNullException(nameof(request));

        var disease = await _dbContext.Diseases
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (disease is null)
        {
            _logger.LogWarning("Disease with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Disease with id {request.Id} not found.");
        }

        // Prevent duplicate DiseaseCode (exclude current entity)
        var duplicate = await _dbContext.Diseases
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DiseaseCode.ToLower() == request.Request.DiseaseCode.Trim().ToLower() && d.Id != request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (duplicate is not null)
            throw new System.InvalidOperationException($"DiseaseCode '{request.Request.DiseaseCode}' already exists.");

        // Update editable properties
        disease.DiseaseCode = request.Request.DiseaseCode.Trim();
        disease.DiseaseName = request.Request.DiseaseName.Trim();
        disease.CropType = request.Request.CropType.Trim();
        disease.Symptoms = request.Request.Symptoms.Trim();
        disease.Causes = request.Request.Causes.Trim();
        disease.Treatment = request.Request.Treatment.Trim();
        disease.Prevention = request.Request.Prevention.Trim();
        disease.Severity = request.Request.Severity.Trim();
        disease.IsActive = request.Request.IsActive;

        disease.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated disease {DiseaseId}", disease.Id);

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
            Message = "Disease updated successfully."
        };
    }
}
