using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Irrigations.Commands.DeleteIrrigation;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Irrigations.Handlers.DeleteIrrigation;

/// <summary>
/// Handler to perform a soft delete of an irrigation by setting IsActive to false.
/// </summary>
public sealed class DeleteIrrigationCommandHandler : IRequestHandler<DeleteIrrigationCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteIrrigationCommandHandler> _logger;

    public DeleteIrrigationCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteIrrigationCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteIrrigationCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var irrigation = await _dbContext.Irrigations
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (irrigation is null)
        {
            _logger.LogWarning("Irrigation with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Irrigation with id {request.Id} not found.");
        }

        irrigation.IsActive = false;
        irrigation.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted irrigation {IrrigationId}", irrigation.Id);
        return true;
    }
}
