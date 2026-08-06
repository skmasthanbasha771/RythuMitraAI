using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Soils.DTOs;
using RythuMitraAI.Application.Soils.Queries.GetSoilById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.GetSoilById;

/// <summary>
/// Handles retrieving a soil by identifier.
/// </summary>
public sealed class GetSoilByIdQueryHandler : IRequestHandler<GetSoilByIdQuery, SoilResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetSoilByIdQueryHandler> _logger;

    public GetSoilByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetSoilByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<SoilResponse> Handle(GetSoilByIdQuery request, CancellationToken cancellationToken)
    {
        var soil = await _dbContext.Soils
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (soil is null)
        {
            _logger.LogWarning("Soil with id {Id} not found", request.Id);
            throw new NotFoundException($"Soil with id {request.Id} not found.");
        }

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
