using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Irrigations.Queries.SearchIrrigations;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.SearchIrrigations;

/// <summary>
/// Handles <see cref="SearchIrrigationsQuery"/> by applying filters, pagination and returning a paged response.
/// </summary>
public sealed class SearchIrrigationsQueryHandler : IRequestHandler<SearchIrrigationsQuery, PagedResponse<IrrigationResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchIrrigationsQueryHandler> _logger;

    public SearchIrrigationsQueryHandler(ApplicationDbContext dbContext, ILogger<SearchIrrigationsQueryHandler> logger)
    {
        _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task<PagedResponse<IrrigationResponse>> Handle(SearchIrrigationsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Irrigation>().AsNoTracking().AsQueryable();

        if (request.IsActive.HasValue)
        {
            query = query.Where(i => i.IsActive == request.IsActive.Value);
        }
        else
        {
            query = query.Where(i => i.IsActive);
        }

        if (!string.IsNullOrWhiteSpace(request.IrrigationCode))
        {
            var code = request.IrrigationCode.Trim().ToLower();
            query = query.Where(i => i.IrrigationCode != null && EF.Functions.Like(i.IrrigationCode.ToLower(), $"%{code}%"));
        }

        if (request.FarmerId.HasValue)
        {
            query = query.Where(i => i.FarmerId == request.FarmerId.Value);
        }

        if (request.CropId.HasValue)
        {
            query = query.Where(i => i.CropId == request.CropId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.IrrigationType))
        {
            var t = request.IrrigationType.Trim().ToLower();
            query = query.Where(i => i.IrrigationType != null && EF.Functions.Like(i.IrrigationType.ToLower(), $"%{t}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.WaterSource))
        {
            var s = request.WaterSource.Trim().ToLower();
            query = query.Where(i => i.WaterSource != null && EF.Functions.Like(i.WaterSource.ToLower(), $"%{s}%"));
        }

        if (request.IrrigationDate.HasValue)
        {
            var dt = request.IrrigationDate.Value.Date;
            query = query.Where(i => i.IrrigationDate.Date == dt);
        }

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var itemsList = await query
            .OrderByDescending(i => i.IrrigationDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = itemsList.Select(i => new IrrigationResponse
        {
            Id = i.Id,
            IrrigationCode = i.IrrigationCode,
            FarmerId = i.FarmerId,
            CropId = i.CropId,
            IrrigationType = i.IrrigationType,
            WaterSource = i.WaterSource,
            IrrigationDate = i.IrrigationDate,
            DurationInMinutes = i.DurationInMinutes,
            WaterQuantity = i.WaterQuantity,
            WaterUnit = i.WaterUnit,
            Remarks = i.Remarks,
            IsActive = i.IsActive,
            CreatedAtUtc = i.CreatedAt,
            Message = null
        });

        _logger.LogDebug("Search returned {Count} irrigations (total {Total})", items.Count(), total);

        return new PagedResponse<IrrigationResponse>(items, pageNumber, pageSize, total);
    }
}
