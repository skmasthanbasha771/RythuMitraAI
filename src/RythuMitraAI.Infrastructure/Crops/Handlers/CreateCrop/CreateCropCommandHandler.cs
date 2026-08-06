using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.Commands.CreateCrop;
using RythuMitraAI.Application.Crops.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.CreateCrop;

/// <summary>
/// Handles <see cref="CreateCropCommand"/> to create and persist a Crop entity.
/// </summary>
public sealed class CreateCropCommandHandler : IRequestHandler<CreateCropCommand, CropResponse>
{
    private readonly IGenericRepository<Crop> _cropRepository;
    private readonly IGenericRepository<Farmer> _farmerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCropCommandHandler> _logger;

    public CreateCropCommandHandler(
        IGenericRepository<Crop> cropRepository,
        IGenericRepository<Farmer> farmerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateCropCommandHandler> logger)
    {
        _cropRepository = cropRepository ?? throw new ArgumentNullException(nameof(cropRepository));
        _farmerRepository = farmerRepository ?? throw new ArgumentNullException(nameof(farmerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CropResponse> Handle(CreateCropCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        var farmer = await _farmerRepository.GetByIdAsync(dto.FarmerId, cancellationToken).ConfigureAwait(false);
        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {dto.FarmerId} does not exist.");

        var existingCrop = await _cropRepository.FindAsync(c => c.CropCode.ToLower() == dto.CropCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existingCrop.Any())
            throw new InvalidOperationException($"CropCode '{dto.CropCode}' already exists.");

        var crop = new Crop
        {
            CropCode = dto.CropCode.Trim(),
            CropName = dto.CropName.Trim(),
            CropCategory = dto.CropCategory.Trim(),
            Season = dto.Season.Trim(),
            SowingDate = dto.SowingDate,
            HarvestDate = dto.HarvestDate,
            Area = dto.Area,
            AreaUnit = dto.AreaUnit.Trim(),
            FarmerId = dto.FarmerId,
            IsActive = dto.IsActive
        };

        crop.SetCreated();

        await _cropRepository.AddAsync(crop, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created crop {CropId} for farmer {FarmerId}", crop.Id, crop.FarmerId);

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
            Message = "Crop created successfully."
        };
    }
}
