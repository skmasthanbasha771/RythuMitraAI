using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Soils.Commands.DeleteSoil;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Soils.Handlers.DeleteSoil;

/// <summary>
/// Handler to perform a soft delete of a soil by setting IsActive to false.
/// </summary>
public sealed class DeleteSoilCommandHandler : IRequestHandler<DeleteSoilCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteSoilCommandHandler> _logger;

    public DeleteSoilCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteSoilCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteSoilCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var soil = await _dbContext.Soils
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (soil is null)
        {
            _logger.LogWarning("Soil with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Soil with id {request.Id} not found.");
        }

        if (!soil.IsActive)
        {
            _logger.LogWarning("Soil with id {Id} is already inactive", request.Id);
            throw new NotFoundException($"Soil with id {request.Id} is already inactive.");
        }

        soil.IsActive = false;
        soil.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted soil {SoilId}", soil.Id);
        return true;
    }
}
