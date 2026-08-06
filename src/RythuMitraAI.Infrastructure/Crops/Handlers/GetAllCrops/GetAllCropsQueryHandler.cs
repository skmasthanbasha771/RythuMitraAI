using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Crops.Queries.GetAllCrops;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.GetAllCrops;

/// <summary>
/// Handles <see cref="GetAllCropsQuery"/> by retrieving active crops from the database.
/// </summary>
public sealed class GetAllCropsQueryHandler : IRequestHandler<GetAllCropsQuery, IEnumerable<CropResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllCropsQueryHandler> _logger;

    public GetAllCropsQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllCropsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<CropResponse>> Handle(GetAllCropsQuery request, CancellationToken cancellationToken)
    {
        var crops = await _dbContext.Crops
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.CropName)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = crops.Select(c => new CropResponse
        {
            Id = c.Id,
            CropCode = c.CropCode,
            CropName = c.CropName,
            CropCategory = c.CropCategory,
            Season = c.Season,
            SowingDate = c.SowingDate,
            HarvestDate = c.HarvestDate,
            Area = c.Area,
            AreaUnit = c.AreaUnit,
            FarmerId = c.FarmerId,
            IsActive = c.IsActive,
            CreatedAtUtc = c.CreatedAt,
            Message = null
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active crops", result.Count);

        return result;
    }
}
