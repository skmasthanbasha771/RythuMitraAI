using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Crops.Queries.SearchCrops;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.SearchCrops;

public sealed class SearchCropsQueryHandler : IRequestHandler<SearchCropsQuery, PagedResponse<CropResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchCropsQueryHandler> _logger;

    public SearchCropsQueryHandler(ApplicationDbContext dbContext, ILogger<SearchCropsQueryHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PagedResponse<CropResponse>> Handle(SearchCropsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Crop>().AsNoTracking().Where(c => c.IsActive);

        if (!string.IsNullOrWhiteSpace(request.CropName))
        {
            var name = request.CropName.Trim().ToLower();
            query = query.Where(c => c.CropName != null && EF.Functions.Like(c.CropName.ToLower(), $"%{name}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.CropCategory))
        {
            var cat = request.CropCategory.Trim().ToLower();
            query = query.Where(c => c.CropCategory != null && EF.Functions.Like(c.CropCategory.ToLower(), $"%{cat}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.Season))
        {
            var s = request.Season.Trim().ToLower();
            query = query.Where(c => c.Season != null && EF.Functions.Like(c.Season.ToLower(), $"%{s}%"));
        }

        if (request.FarmerId.HasValue)
        {
            query = query.Where(c => c.FarmerId == request.FarmerId.Value);
        }

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var crops = await query
            .OrderBy(c => c.CropName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = crops.Select(c => new CropResponse
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
            CreatedAtUtc = c.CreatedAt
        });

        return new PagedResponse<CropResponse>(items, pageNumber, pageSize, total);
    }
}
