using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Fertilizers.Commands.UpdateFertilizer;
using RythuMitraAI.Application.Fertilizers.DTOs;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.UpdateFertilizer;

public sealed class UpdateFertilizerCommandHandler : IRequestHandler<UpdateFertilizerCommand, FertilizerResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateFertilizerCommandHandler> _logger;

    public UpdateFertilizerCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateFertilizerCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FertilizerResponse> Handle(UpdateFertilizerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var fertilizer = await _dbContext.Fertilizers
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (fertilizer is null)
        {
            _logger.LogWarning("Fertilizer with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Fertilizer with id {request.Id} not found.");
        }

        // Update editable fields only
        fertilizer.FertilizerName = request.Request.FertilizerName.Trim();
        fertilizer.Brand = request.Request.Brand.Trim();
        fertilizer.FertilizerType = request.Request.FertilizerType.Trim();
        fertilizer.Nitrogen = request.Request.Nitrogen;
        fertilizer.Phosphorus = request.Request.Phosphorus;
        fertilizer.Potassium = request.Request.Potassium;
        fertilizer.RecommendedCrop = request.Request.RecommendedCrop.Trim();
        fertilizer.RecommendedSoil = request.Request.RecommendedSoil.Trim();
        fertilizer.Description = request.Request.Description?.Trim();
        fertilizer.IsActive = request.Request.IsActive;

        fertilizer.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated fertilizer {FertilizerId}", fertilizer.Id);

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
