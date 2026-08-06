using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Soils.DTOs;
using RythuMitraAI.Application.Soils.Queries.GetAllSoils;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.GetAllSoils;

/// <summary>
/// Handles <see cref="GetAllSoilsQuery"/> and returns active soils ordered by TestDate desc.
/// </summary>
public sealed class GetAllSoilsQueryHandler : IRequestHandler<GetAllSoilsQuery, IEnumerable<SoilResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllSoilsQueryHandler> _logger;

    public GetAllSoilsQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllSoilsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<SoilResponse>> Handle(GetAllSoilsQuery request, CancellationToken cancellationToken)
    {
        var soils = await _dbContext.Soils
            .AsNoTracking()
            .Where(s => s.IsActive)
            .OrderByDescending(s => s.TestDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = soils.Select(s => new SoilResponse
        {
            Id = s.Id,
            SoilCode = s.SoilCode,
            FarmerId = s.FarmerId,
            PH = s.PH,
            Moisture = s.Moisture,
            Nitrogen = s.Nitrogen,
            Phosphorus = s.Phosphorus,
            Potassium = s.Potassium,
            OrganicCarbon = s.OrganicCarbon,
            TestDate = s.TestDate,
            Remarks = s.Remarks,
            IsActive = s.IsActive,
            CreatedAtUtc = s.CreatedAt
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active soils", result.Count);

        return result;
    }
}
