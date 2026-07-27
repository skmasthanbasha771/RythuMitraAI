using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Application.Farmers.Queries.GetAllFarmers;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.GetAllFarmers;

/// <summary>
/// Handles GetAllFarmersQuery by retrieving active farmers and mapping to FarmerListResponse.
/// </summary>
public sealed class GetAllFarmersQueryHandler : IRequestHandler<GetAllFarmersQuery, List<FarmerListResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllFarmersQueryHandler> _logger;

    public GetAllFarmersQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllFarmersQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<FarmerListResponse>> Handle(GetAllFarmersQuery request, CancellationToken cancellationToken)
    {
        var farmers = await _dbContext.Farmers
            .AsNoTracking()
            .Where(f => f.IsActive)
            .OrderBy(f => f.FirstName)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = farmers.Select(f => new FarmerListResponse
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

        _logger.LogDebug("Retrieved {Count} active farmers", result.Count);

        return result;
    }
}
