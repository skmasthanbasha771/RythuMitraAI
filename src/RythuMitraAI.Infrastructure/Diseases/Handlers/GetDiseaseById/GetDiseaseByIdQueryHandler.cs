using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Diseases.Queries.GetDiseaseById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.GetDiseaseById;

/// <summary>
/// Handles retrieving a disease by identifier.
/// </summary>
public sealed class GetDiseaseByIdQueryHandler : IRequestHandler<GetDiseaseByIdQuery, DiseaseResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetDiseaseByIdQueryHandler> _logger;

    public GetDiseaseByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetDiseaseByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<DiseaseResponse> Handle(GetDiseaseByIdQuery request, CancellationToken cancellationToken)
    {
        var disease = await _dbContext.Diseases
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (disease is null)
        {
            _logger.LogWarning("Disease with id {Id} not found", request.Id);
            throw new NotFoundException($"Disease with id {request.Id} not found.");
        }

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
            Message = null
        };
    }
}
