using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Crops.Queries.GetCropById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.GetCropById;

/// <summary>
/// Handles retrieving a crop by identifier.
/// </summary>
public sealed class GetCropByIdQueryHandler : IRequestHandler<GetCropByIdQuery, CropResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetCropByIdQueryHandler> _logger;

    public GetCropByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetCropByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<CropResponse> Handle(GetCropByIdQuery request, CancellationToken cancellationToken)
    {
        var crop = await _dbContext.Crops
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (crop is null)
        {
            _logger.LogWarning("Crop with id {Id} not found", request.Id);
            throw new NotFoundException($"Crop with id {request.Id} not found.");
        }

        return new CropResponse
        {
            Id = crop.Id,
            CropCode = crop.CropCode,
            CropName = crop.CropName,
            CropCategory = crop.CropCategory,
            Season = crop.Season,
            SowingDate = crop.SowingDate,
            HarvestDate = crop.HarvestDate,
            Area = crop.Area,
            AreaUnit = crop.AreaUnit,
            FarmerId = crop.FarmerId,
            IsActive = crop.IsActive,
            CreatedAtUtc = crop.CreatedAt,
            Message = null
        };
    }
}
