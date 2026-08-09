using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Fertilizers.DTOs;
using RythuMitraAI.Application.Fertilizers.Queries.GetFertilizerById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.GetFertilizerById;

public sealed class GetFertilizerByIdQueryHandler : IRequestHandler<GetFertilizerByIdQuery, FertilizerResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetFertilizerByIdQueryHandler> _logger;

    public GetFertilizerByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetFertilizerByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<FertilizerResponse> Handle(GetFertilizerByIdQuery request, CancellationToken cancellationToken)
    {
        var fertilizer = await _dbContext.Fertilizers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (fertilizer is null)
        {
            _logger.LogWarning("Fertilizer with id {Id} not found", request.Id);
            throw new NotFoundException($"Fertilizer with id {request.Id} not found.");
        }

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
