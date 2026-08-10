using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.DTOs;
using RythuMitraAI.Application.Irrigations.Queries.GetIrrigationById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.GetIrrigationById;

/// <summary>
/// Handles retrieving an irrigation by identifier.
/// </summary>
public sealed class GetIrrigationByIdQueryHandler : IRequestHandler<GetIrrigationByIdQuery, IrrigationResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetIrrigationByIdQueryHandler> _logger;

    public GetIrrigationByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetIrrigationByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IrrigationResponse> Handle(GetIrrigationByIdQuery request, CancellationToken cancellationToken)
    {
        var irrigation = await _dbContext.Irrigations
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (irrigation is null)
        {
            _logger.LogWarning("Irrigation with id {Id} not found", request.Id);
            throw new NotFoundException($"Irrigation with id {request.Id} not found.");
        }

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
            Message = null
        };
    }
}
