using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Soils.Commands.UpdateSoil;
using RythuMitraAI.Application.Soils.DTOs;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.UpdateSoil;

/// <summary>
/// Handles updating an existing soil record.
/// </summary>
public sealed class UpdateSoilCommandHandler : IRequestHandler<UpdateSoilCommand, SoilResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateSoilCommandHandler> _logger;

    public UpdateSoilCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateSoilCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<SoilResponse> Handle(UpdateSoilCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var soil = await _dbContext.Soils
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (soil is null)
        {
            _logger.LogWarning("Soil with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Soil with id {request.Id} not found.");
        }

        // Validate farmer exists
        var farmer = await _dbContext.Farmers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Request.FarmerId, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {request.Request.FarmerId} does not exist.");

        // Update editable fields only
        soil.FarmerId = request.Request.FarmerId;
        soil.PH = request.Request.PH;
        soil.Moisture = request.Request.Moisture;
        soil.Nitrogen = request.Request.Nitrogen;
        soil.Phosphorus = request.Request.Phosphorus;
        soil.Potassium = request.Request.Potassium;
        soil.OrganicCarbon = request.Request.OrganicCarbon;
        soil.TestDate = request.Request.TestDate;
        soil.Remarks = request.Request.Remarks?.Trim();
        soil.IsActive = request.Request.IsActive;

        soil.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated soil {SoilId}", soil.Id);

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
            CreatedAtUtc = soil.CreatedAt,
        };
    }
}
