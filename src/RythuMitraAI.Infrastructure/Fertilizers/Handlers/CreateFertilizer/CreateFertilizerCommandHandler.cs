using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Fertilizers.Commands.CreateFertilizer;
using RythuMitraAI.Application.Fertilizers.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.CreateFertilizer;

public sealed class CreateFertilizerCommandHandler : IRequestHandler<CreateFertilizerCommand, FertilizerResponse>
{
    private readonly IGenericRepository<Fertilizer> _fertilizerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateFertilizerCommandHandler> _logger;

    public CreateFertilizerCommandHandler(
        IGenericRepository<Fertilizer> fertilizerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateFertilizerCommandHandler> logger)
    {
        _fertilizerRepository = fertilizerRepository ?? throw new ArgumentNullException(nameof(fertilizerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FertilizerResponse> Handle(CreateFertilizerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var dto = request.Request;

        var existing = await _fertilizerRepository.FindAsync(f => f.FertilizerCode.ToLower() == dto.FertilizerCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
            throw new InvalidOperationException($"FertilizerCode '{dto.FertilizerCode}' already exists.");

        var fertilizer = new Fertilizer
        {
            FertilizerCode = dto.FertilizerCode.Trim(),
            FertilizerName = dto.FertilizerName.Trim(),
            Brand = dto.Brand.Trim(),
            FertilizerType = dto.FertilizerType.Trim(),
            Nitrogen = dto.Nitrogen,
            Phosphorus = dto.Phosphorus,
            Potassium = dto.Potassium,
            RecommendedCrop = dto.RecommendedCrop.Trim(),
            RecommendedSoil = dto.RecommendedSoil.Trim(),
            Description = dto.Description?.Trim(),
            IsActive = dto.IsActive
        };

        fertilizer.SetCreated();

        await _fertilizerRepository.AddAsync(fertilizer, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created fertilizer {FertilizerId} ({Code})", fertilizer.Id, fertilizer.FertilizerCode);

        return new FertilizerResponse
        {
            Id = fertilizer.Id,
            FertilizerCode = fertilizer.FertilizerCode,
            FertilizerName = fertilizer.FertilizerName,
            Brand = fertilizer.Brand,
            FertilizerType = fertilizer.FertilizerType,
            Nitrogen = fertilizer.Nitrogen,
            Phosphorus = fertilizer.Phosphorus,
            Potassium = fertilizer.Potassium,
            RecommendedCrop = fertilizer.RecommendedCrop,
            RecommendedSoil = fertilizer.RecommendedSoil,
            Description = fertilizer.Description,
            IsActive = fertilizer.IsActive,
            CreatedAtUtc = fertilizer.CreatedAt
        };
    }
}
