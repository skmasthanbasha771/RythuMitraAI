using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Farmers.Commands.DeleteFarmer;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.DeleteFarmer;

/// <summary>
/// Handler to perform a soft delete of a farmer by setting IsActive to false.
/// </summary>
public sealed class DeleteFarmerCommandHandler : IRequestHandler<DeleteFarmerCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteFarmerCommandHandler> _logger;

    public DeleteFarmerCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteFarmerCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteFarmerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var farmer = await _dbContext.Farmers
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
        {
            _logger.LogWarning("Farmer with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Farmer with id {request.Id} not found.");
        }

        // Soft delete
        farmer.IsActive = false;
        farmer.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted farmer {Id}", farmer.Id);

        return true;
    }
}
