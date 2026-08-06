using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Soils.DTOs;
using RythuMitraAI.Application.Soils.Queries.SearchSoils;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.SearchSoils;

public sealed class SearchSoilsQueryHandler : IRequestHandler<SearchSoilsQuery, PagedResponse<SoilResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchSoilsQueryHandler> _logger;

    public SearchSoilsQueryHandler(ApplicationDbContext dbContext, ILogger<SearchSoilsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedResponse<SoilResponse>> Handle(SearchSoilsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Soil>().AsNoTracking().Where(s => s.IsActive);

        if (!string.IsNullOrWhiteSpace(request.SoilCode))
        {
            var code = request.SoilCode.Trim().ToLower();
            query = query.Where(s => s.SoilCode != null && EF.Functions.Like(s.SoilCode.ToLower(), $"%{code}%"));
        }

        if (request.FarmerId.HasValue)
            query = query.Where(s => s.FarmerId == request.FarmerId.Value);

        if (request.TestDate.HasValue)
            query = query.Where(s => s.TestDate.Date == request.TestDate.Value.Date);

        if (request.MinPH.HasValue)
            query = query.Where(s => s.PH >= request.MinPH.Value);
        if (request.MaxPH.HasValue)
            query = query.Where(s => s.PH <= request.MaxPH.Value);

        if (request.MinNitrogen.HasValue)
            query = query.Where(s => s.Nitrogen >= request.MinNitrogen.Value);
        if (request.MaxNitrogen.HasValue)
            query = query.Where(s => s.Nitrogen <= request.MaxNitrogen.Value);

        if (request.MinPhosphorus.HasValue)
            query = query.Where(s => s.Phosphorus >= request.MinPhosphorus.Value);
        if (request.MaxPhosphorus.HasValue)
            query = query.Where(s => s.Phosphorus <= request.MaxPhosphorus.Value);

        if (request.MinPotassium.HasValue)
            query = query.Where(s => s.Potassium >= request.MinPotassium.Value);
        if (request.MaxPotassium.HasValue)
            query = query.Where(s => s.Potassium <= request.MaxPotassium.Value);

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var soils = await query
            .OrderByDescending(s => s.TestDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = soils.Select(s => new SoilResponse
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
        });

        _logger.LogDebug("Search returned {Count} soils (total {Total})", items.Count(), total);

        return new PagedResponse<SoilResponse>(items, pageNumber, pageSize, total);
    }
}
