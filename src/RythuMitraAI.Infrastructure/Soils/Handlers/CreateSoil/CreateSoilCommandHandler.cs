using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Soils.Commands.CreateSoil;
using RythuMitraAI.Application.Soils.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.CreateSoil;

/// <summary>
/// Handles <see cref="CreateSoilCommand"/> to create and persist a Soil entity.
/// </summary>
public sealed class CreateSoilCommandHandler : IRequestHandler<CreateSoilCommand, SoilResponse>
{
    private readonly IGenericRepository<Soil> _soilRepository;
    private readonly IGenericRepository<Farmer> _farmerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateSoilCommandHandler> _logger;

    public CreateSoilCommandHandler(
        IGenericRepository<Soil> soilRepository,
        IGenericRepository<Farmer> farmerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateSoilCommandHandler> logger)
    {
        _soilRepository = soilRepository ?? throw new ArgumentNullException(nameof(soilRepository));
        _farmerRepository = farmerRepository ?? throw new ArgumentNullException(nameof(farmerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<SoilResponse> Handle(CreateSoilCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        var farmer = await _farmerRepository.GetByIdAsync(dto.FarmerId, cancellationToken).ConfigureAwait(false);
        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {dto.FarmerId} does not exist.");

        var existing = await _soilRepository.FindAsync(s => s.SoilCode.ToLower() == dto.SoilCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
            throw new InvalidOperationException($"SoilCode '{dto.SoilCode}' already exists.");

        var soil = new Soil
        {
            SoilCode = dto.SoilCode.Trim(),
            FarmerId = dto.FarmerId,
            PH = dto.PH,
            Moisture = dto.Moisture,
            Nitrogen = dto.Nitrogen,
            Phosphorus = dto.Phosphorus,
            Potassium = dto.Potassium,
            OrganicCarbon = dto.OrganicCarbon,
            TestDate = dto.TestDate,
            Remarks = dto.Remarks?.Trim(),
            IsActive = dto.IsActive
        };

        soil.SetCreated();

        await _soilRepository.AddAsync(soil, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created soil {SoilId} for farmer {FarmerId}", soil.Id, soil.FarmerId);

        return new SoilResponse
        {
            Id = soil.Id,
            SoilCode = soil.SoilCode,
            FarmerId = soil.FarmerId,
            PH = soil.PH,
            Moisture = soil.Moisture,
            Nitrogen = soil.Nitrogen,
            Phosphorus = soil.Phosphorus,
            Potassium = soil.Potassium,
            OrganicCarbon = soil.OrganicCarbon,
            TestDate = soil.TestDate,
            Remarks = soil.Remarks,
            IsActive = soil.IsActive,
            CreatedAtUtc = soil.CreatedAt
        };
    }
}
