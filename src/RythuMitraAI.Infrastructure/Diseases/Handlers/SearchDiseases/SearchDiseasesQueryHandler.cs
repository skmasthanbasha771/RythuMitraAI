using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Diseases.DTOs;
using RythuMitraAI.Application.Diseases.Queries.SearchDiseases;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.SearchDiseases;

/// <summary>
/// Handles <see cref="SearchDiseasesQuery"/> by applying filters, pagination and returning a paged response.
/// </summary>
public sealed class SearchDiseasesQueryHandler : IRequestHandler<SearchDiseasesQuery, PagedResponse<DiseaseResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchDiseasesQueryHandler> _logger;

    public SearchDiseasesQueryHandler(ApplicationDbContext dbContext, ILogger<SearchDiseasesQueryHandler> logger)
    {
        _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task<PagedResponse<DiseaseResponse>> Handle(SearchDiseasesQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Disease>().AsNoTracking().AsQueryable();

        // Default to active records when IsActive not provided
        if (request.IsActive.HasValue)
        {
            query = query.Where(d => d.IsActive == request.IsActive.Value);
        }
        else
        {
            query = query.Where(d => d.IsActive);
        }

        if (!string.IsNullOrWhiteSpace(request.DiseaseCode))
        {
            var code = request.DiseaseCode.Trim().ToLower();
            query = query.Where(d => d.DiseaseCode != null && EF.Functions.Like(d.DiseaseCode.ToLower(), $"%{code}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.DiseaseName))
        {
            var name = request.DiseaseName.Trim().ToLower();
            query = query.Where(d => d.DiseaseName != null && EF.Functions.Like(d.DiseaseName.ToLower(), $"%{name}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.CropType))
        {
            var crop = request.CropType.Trim().ToLower();
            query = query.Where(d => d.CropType != null && EF.Functions.Like(d.CropType.ToLower(), $"%{crop}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.Severity))
        {
            var sev = request.Severity.Trim().ToLower();
            query = query.Where(d => d.Severity != null && EF.Functions.Like(d.Severity.ToLower(), $"%{sev}%"));
        }

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var diseases = await query
            .OrderBy(d => d.DiseaseName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = diseases.Select(d => new DiseaseResponse
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
        });

        _logger.LogDebug("Search returned {Count} diseases (total {Total})", items.Count(), total);

        return new PagedResponse<DiseaseResponse>(items, pageNumber, pageSize, total);
    }
}
