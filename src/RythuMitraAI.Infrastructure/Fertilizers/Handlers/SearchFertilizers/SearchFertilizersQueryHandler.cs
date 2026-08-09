using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Fertilizers.DTOs;
using RythuMitraAI.Application.Fertilizers.Queries.SearchFertilizers;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.SearchFertilizers;

public sealed class SearchFertilizersQueryHandler : IRequestHandler<SearchFertilizersQuery, PagedResponse<FertilizerResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchFertilizersQueryHandler> _logger;

    public SearchFertilizersQueryHandler(ApplicationDbContext dbContext, ILogger<SearchFertilizersQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedResponse<FertilizerResponse>> Handle(SearchFertilizersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Fertilizer>().AsNoTracking().Where(f => f.IsActive);

        if (!string.IsNullOrWhiteSpace(request.FertilizerCode))
        {
            var code = request.FertilizerCode.Trim().ToLower();
            query = query.Where(f => f.FertilizerCode != null && EF.Functions.Like(f.FertilizerCode.ToLower(), $"%{code}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.FertilizerName))
        {
            var name = request.FertilizerName.Trim().ToLower();
            query = query.Where(f => f.FertilizerName != null && EF.Functions.Like(f.FertilizerName.ToLower(), $"%{name}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            var brand = request.Brand.Trim().ToLower();
            query = query.Where(f => f.Brand != null && EF.Functions.Like(f.Brand.ToLower(), $"%{brand}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.FertilizerType))
        {
            var type = request.FertilizerType.Trim().ToLower();
            query = query.Where(f => f.FertilizerType != null && EF.Functions.Like(f.FertilizerType.ToLower(), $"%{type}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.RecommendedCrop))
        {
            var crop = request.RecommendedCrop.Trim().ToLower();
            query = query.Where(f => f.RecommendedCrop != null && EF.Functions.Like(f.RecommendedCrop.ToLower(), $"%{crop}%"));
        }

        if (!string.IsNullOrWhiteSpace(request.RecommendedSoil))
        {
            var soil = request.RecommendedSoil.Trim().ToLower();
            query = query.Where(f => f.RecommendedSoil != null && EF.Functions.Like(f.RecommendedSoil.ToLower(), $"%{soil}%"));
        }

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var items = await query
            .OrderBy(f => f.FertilizerName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var resultItems = items.Select(f => new FertilizerResponse
        {
            Id = f.Id,
            FertilizerCode = f.FertilizerCode,
            FertilizerName = f.FertilizerName,
            Brand = f.Brand,
            FertilizerType = f.FertilizerType,
            Nitrogen = f.Nitrogen,
            Phosphorus = f.Phosphorus,
            Potassium = f.Potassium,
            RecommendedCrop = f.RecommendedCrop,
            RecommendedSoil = f.RecommendedSoil,
            Description = f.Description,
            IsActive = f.IsActive,
            CreatedAtUtc = f.CreatedAt
        });

        _logger.LogDebug("Search returned {Count} fertilizers (total {Total})", resultItems.Count(), total);

        return new PagedResponse<FertilizerResponse>(resultItems, pageNumber, pageSize, total);
    }
}
