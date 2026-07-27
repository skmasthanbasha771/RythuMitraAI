using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Application.Farmers.Queries.SearchFarmers;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.SearchFarmers;

public sealed class SearchFarmersQueryHandler : IRequestHandler<SearchFarmersQuery, PagedResponse<FarmerListResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchFarmersQueryHandler> _logger;

    public SearchFarmersQueryHandler(ApplicationDbContext dbContext, ILogger<SearchFarmersQueryHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PagedResponse<FarmerListResponse>> Handle(SearchFarmersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Farmers.AsNoTracking().Where(f => f.IsActive);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var s = request.Search.Trim().ToLower();
            query = query.Where(f => (f.FarmerCode != null && EF.Functions.Like(f.FarmerCode.ToLower(), $"%{s}%"))
                                     || (f.FirstName != null && EF.Functions.Like(f.FirstName.ToLower(), $"%{s}%"))
                                     || (f.LastName != null && EF.Functions.Like(f.LastName.ToLower(), $"%{s}%"))
                                     || (f.PhoneNumber != null && EF.Functions.Like(f.PhoneNumber.ToLower(), $"%{s}%")));
        }

        if (!string.IsNullOrWhiteSpace(request.District))
        {
            var d = request.District.Trim().ToLower();
            query = query.Where(f => f.District != null && EF.Functions.Like(f.District.ToLower(), $"%{d}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.Village))
        {
            var v = request.Village.Trim().ToLower();
            query = query.Where(f => f.Village != null && EF.Functions.Like(f.Village.ToLower(), $"%{v}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.State))
        {
            var st = request.State.Trim().ToLower();
            query = query.Where(f => f.State != null && EF.Functions.Like(f.State.ToLower(), $"%{st}%"));
        }

        var totalRecords = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var farmers = await query
            .OrderBy(f => f.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = farmers.Select(f => new FarmerListResponse
        {
            Items = new[]
            {
                new FarmerResponse
                {
                    Id = f.Id,
                    FarmerCode = f.FarmerCode,
                    FirstName = f.FirstName,
                    LastName = f.LastName,
                    PhoneNumber = f.PhoneNumber,
                    Email = f.Email,
                    Village = f.Village,
                    District = f.District,
                    State = f.State,
                    LandArea = f.LandArea,
                    LandUnit = f.LandUnit,
                    IsActive = f.IsActive,
                    CreatedAtUtc = f.CreatedAt,
                    UpdatedAtUtc = f.ModifiedAt
                }
            },
            TotalCount = null
        }).ToList();

        _logger.LogDebug("Search returned {Count} farmers (total {Total})", items.Count, totalRecords);

        return new PagedResponse<FarmerListResponse>(items, pageNumber, pageSize, totalRecords);
    }
}
