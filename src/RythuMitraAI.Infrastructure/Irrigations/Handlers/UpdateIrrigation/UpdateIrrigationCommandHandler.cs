using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.Commands.UpdateIrrigation;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.UpdateIrrigation;

/// <summary>
/// Handles updating an existing irrigation.
/// </summary>
public sealed class UpdateIrrigationCommandHandler : IRequestHandler<UpdateIrrigationCommand, IrrigationResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateIrrigationCommandHandler> _logger;

    public UpdateIrrigationCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateIrrigationCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task<IrrigationResponse> Handle(UpdateIrrigationCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new System.ArgumentNullException(nameof(request));

        var irrigation = await _dbContext.Irrigations
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (irrigation is null)
        {
            _logger.LogWarning("Irrigation with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Irrigation with id {request.Id} not found.");
        }

        // Verify farmer exists
        var farmer = await _dbContext.Farmers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Request.FarmerId, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
            throw new System.InvalidOperationException($"Farmer with id {request.Request.FarmerId} does not exist.");

        // Verify crop exists
        var crop = await _dbContext.Crops
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Request.CropId, cancellationToken)
            .ConfigureAwait(false);

        if (crop is null)
            throw new System.InvalidOperationException($"Crop with id {request.Request.CropId} does not exist.");

        // Prevent duplicate IrrigationCode (exclude current entity)
        var duplicate = await _dbContext.Irrigations
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IrrigationCode.ToLower() == request.Request.IrrigationCode.Trim().ToLower() && i.Id != request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (duplicate is not null)
            throw new System.InvalidOperationException($"IrrigationCode '{request.Request.IrrigationCode}' already exists.");

        // Update editable properties
        irrigation.IrrigationCode = request.Request.IrrigationCode.Trim();
        irrigation.FarmerId = request.Request.FarmerId;
        irrigation.CropId = request.Request.CropId;
        irrigation.IrrigationType = request.Request.IrrigationType.Trim();
        irrigation.WaterSource = request.Request.WaterSource.Trim();
        irrigation.IrrigationDate = request.Request.IrrigationDate;
        irrigation.DurationInMinutes = request.Request.DurationInMinutes;
        irrigation.WaterQuantity = request.Request.WaterQuantity;
        irrigation.WaterUnit = request.Request.WaterUnit.Trim();
        irrigation.Remarks = request.Request.Remarks?.Trim();
        irrigation.IsActive = request.Request.IsActive;

        irrigation.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated irrigation {IrrigationId}", irrigation.Id);

        return new IrrigationResponse
        {
            Id = irrigation.Id,
            IrrigationCode = irrigation.IrrigationCode,
            FarmerId = irrigation.FarmerId,
            CropId = irrigation.CropId,
            IrrigationType = irrigation.IrrigationType,
            WaterSource = irrigation.WaterSource,
            IrrigationDate = irrigation.IrrigationDate,
            DurationInMinutes = irrigation.DurationInMinutes,
            WaterQuantity = irrigation.WaterQuantity,
            WaterUnit = irrigation.WaterUnit,
            Remarks = irrigation.Remarks,
            IsActive = irrigation.IsActive,
            CreatedAtUtc = irrigation.CreatedAt,
            Message = "Irrigation updated successfully."
        };
    }
}
