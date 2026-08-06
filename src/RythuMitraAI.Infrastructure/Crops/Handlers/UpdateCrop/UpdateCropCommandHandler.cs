using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.Commands.UpdateCrop;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.UpdateCrop;

/// <summary>
/// Handles updating an existing crop.
/// </summary>
public sealed class UpdateCropCommandHandler : IRequestHandler<UpdateCropCommand, CropResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateCropCommandHandler> _logger;

    public UpdateCropCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateCropCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CropResponse> Handle(UpdateCropCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var crop = await _dbContext.Crops
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (crop is null)
        {
            _logger.LogWarning("Crop with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Crop with id {request.Id} not found.");
        }

        var farmer = await _dbContext.Farmers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Request.FarmerId, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {request.Request.FarmerId} does not exist.");

        crop.CropName = request.Request.CropName.Trim();
        crop.CropCategory = request.Request.CropCategory.Trim();
        crop.Season = request.Request.Season.Trim();
        crop.SowingDate = request.Request.SowingDate;
        crop.HarvestDate = request.Request.HarvestDate;
        crop.Area = request.Request.Area;
        crop.AreaUnit = request.Request.AreaUnit.Trim();
        crop.FarmerId = request.Request.FarmerId;
        crop.IsActive = request.Request.IsActive;

        crop.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated crop {CropId}", crop.Id);

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
            Message = "Crop updated successfully."
        };
    }
}
