using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Fertilizers.Commands.DeleteFertilizer;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Fertilizers.Handlers.DeleteFertilizer;

public sealed class DeleteFertilizerCommandHandler : IRequestHandler<DeleteFertilizerCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteFertilizerCommandHandler> _logger;

    public DeleteFertilizerCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteFertilizerCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteFertilizerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var fertilizer = await _dbContext.Fertilizers
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (fertilizer is null)
        {
            _logger.LogWarning("Fertilizer with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Fertilizer with id {request.Id} not found.");
        }

        fertilizer.IsActive = false;
        fertilizer.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted fertilizer {FertilizerId}", fertilizer.Id);

        return true;
    }
}
