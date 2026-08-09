using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Fertilizers.DTOs;
using RythuMitraAI.Application.Fertilizers.Queries.GetAllFertilizers;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.GetAllFertilizers;

public sealed class GetAllFertilizersQueryHandler : IRequestHandler<GetAllFertilizersQuery, IEnumerable<FertilizerResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllFertilizersQueryHandler> _logger;

    public GetAllFertilizersQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllFertilizersQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<FertilizerResponse>> Handle(GetAllFertilizersQuery request, CancellationToken cancellationToken)
    {
        var fertilizers = await _dbContext.Fertilizers
            .AsNoTracking()
            .Where(f => f.IsActive)
            .OrderBy(f => f.FertilizerName)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = fertilizers.Select(f => new FertilizerResponse
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
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active fertilizers", result.Count);

        return result;
    }
}
