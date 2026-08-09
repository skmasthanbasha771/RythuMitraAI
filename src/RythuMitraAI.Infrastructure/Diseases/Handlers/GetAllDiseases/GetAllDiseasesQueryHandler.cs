using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Diseases.Queries.GetAllDiseases;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.GetAllDiseases;

/// <summary>
/// Handles <see cref="GetAllDiseasesQuery"/> by retrieving active diseases from the database.
/// </summary>
public sealed class GetAllDiseasesQueryHandler : IRequestHandler<GetAllDiseasesQuery, IEnumerable<DiseaseResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllDiseasesQueryHandler> _logger;

    public GetAllDiseasesQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllDiseasesQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<DiseaseResponse>> Handle(GetAllDiseasesQuery request, CancellationToken cancellationToken)
    {
        var diseases = await _dbContext.Diseases
            .AsNoTracking()
            .Where(d => d.IsActive)
            .OrderBy(d => d.DiseaseName)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = diseases.Select(d => new DiseaseResponse
        {
            Id = d.Id,
            DiseaseCode = d.DiseaseCode,
            DiseaseName = d.DiseaseName,
            CropType = d.CropType,
            Symptoms = d.Symptoms,
            Causes = d.Causes,
            Treatment = d.Treatment,
            Prevention = d.Prevention,
            Severity = d.Severity,
            IsActive = d.IsActive,
            CreatedAtUtc = d.CreatedAt,
            Message = null
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active diseases", result.Count);

        return result;
    }
}
