using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.Commands.CreateIrrigation;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.CreateIrrigation;

/// <summary>
/// Handles <see cref="CreateIrrigationCommand"/> to create and persist an Irrigation entity.
/// </summary>
public sealed class CreateIrrigationCommandHandler : IRequestHandler<CreateIrrigationCommand, IrrigationResponse>
{
    private readonly IGenericRepository<Irrigation> _irrigationRepository;
    private readonly IGenericRepository<Farmer> _farmerRepository;
    private readonly IGenericRepository<Crop> _cropRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateIrrigationCommandHandler> _logger;

    public CreateIrrigationCommandHandler(
        IGenericRepository<Irrigation> irrigationRepository,
        IGenericRepository<Farmer> farmerRepository,
        IGenericRepository<Crop> cropRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateIrrigationCommandHandler> logger)
    {
        _irrigationRepository = irrigationRepository ?? throw new System.ArgumentNullException(nameof(irrigationRepository));
        _farmerRepository = farmerRepository ?? throw new System.ArgumentNullException(nameof(farmerRepository));
        _cropRepository = cropRepository ?? throw new System.ArgumentNullException(nameof(cropRepository));
        _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task<IrrigationResponse> Handle(CreateIrrigationCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new System.ArgumentNullException(nameof(request));

        var dto = request.Request;

        // Verify farmer exists
        var farmer = await _farmerRepository.GetByIdAsync(dto.FarmerId, cancellationToken).ConfigureAwait(false);
        if (farmer is null)
            throw new System.InvalidOperationException($"Farmer with id {dto.FarmerId} does not exist.");

        // Verify crop exists
        var crop = await _cropRepository.GetByIdAsync(dto.CropId, cancellationToken).ConfigureAwait(false);
        if (crop is null)
            throw new System.InvalidOperationException($"Crop with id {dto.CropId} does not exist.");

        // Prevent duplicate IrrigationCode
        var existing = await _irrigationRepository.FindAsync(i => i.IrrigationCode.ToLower() == dto.IrrigationCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
            throw new System.InvalidOperationException($"IrrigationCode '{dto.IrrigationCode}' already exists.");

        var irrigation = new Irrigation
        {
            IrrigationCode = dto.IrrigationCode.Trim(),
            FarmerId = dto.FarmerId,
            CropId = dto.CropId,
            IrrigationType = dto.IrrigationType.Trim(),
            WaterSource = dto.WaterSource.Trim(),
            IrrigationDate = dto.IrrigationDate,
            DurationInMinutes = dto.DurationInMinutes,
            WaterQuantity = dto.WaterQuantity,
            WaterUnit = dto.WaterUnit.Trim(),
            Remarks = dto.Remarks?.Trim(),
            IsActive = dto.IsActive
        };

        irrigation.SetCreated();

        await _irrigationRepository.AddAsync(irrigation, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created irrigation {IrrigationId} for farmer {FarmerId}", irrigation.Id, irrigation.FarmerId);

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
            Message = "Irrigation created successfully."
        };
    }
}
