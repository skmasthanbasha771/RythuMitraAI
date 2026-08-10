using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Irrigations.Queries.GetAllIrrigations;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.GetAllIrrigations;

/// <summary>
/// Handles <see cref="GetAllIrrigationsQuery"/> by retrieving active irrigations from the database.
/// </summary>
public sealed class GetAllIrrigationsQueryHandler : IRequestHandler<GetAllIrrigationsQuery, IEnumerable<IrrigationResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllIrrigationsQueryHandler> _logger;

    public GetAllIrrigationsQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllIrrigationsQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<IrrigationResponse>> Handle(GetAllIrrigationsQuery request, CancellationToken cancellationToken)
    {
        var irrigations = await _dbContext.Irrigations
            .AsNoTracking()
            .Where(i => i.IsActive)
            .OrderByDescending(i => i.IrrigationDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = irrigations.Select(i => new IrrigationResponse
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
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active irrigations", result.Count);

        return result;
    }
}
