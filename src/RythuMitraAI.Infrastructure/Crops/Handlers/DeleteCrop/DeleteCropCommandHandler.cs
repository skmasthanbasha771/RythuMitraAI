using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Crops.Commands.DeleteCrop;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Crops.Handlers.DeleteCrop;

/// <summary>
/// Handler to perform a soft delete of a crop by setting IsActive to false.
/// </summary>
public sealed class DeleteCropCommandHandler : IRequestHandler<DeleteCropCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteCropCommandHandler> _logger;

    public DeleteCropCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteCropCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteCropCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var crop = await _dbContext.Crops
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (crop is null)
        {
            _logger.LogWarning("Crop with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Crop with id {request.Id} not found.");
        }

        crop.IsActive = false;
        crop.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted crop {CropId}", crop.Id);
        return true;
    }
}
