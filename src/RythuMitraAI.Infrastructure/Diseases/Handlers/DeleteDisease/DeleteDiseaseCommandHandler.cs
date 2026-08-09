using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Diseases.Commands.DeleteDisease;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Diseases.Handlers.DeleteDisease;

/// <summary>
/// Handler to perform a soft delete of a disease by setting IsActive to false.
/// </summary>
public sealed class DeleteDiseaseCommandHandler : IRequestHandler<DeleteDiseaseCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteDiseaseCommandHandler> _logger;

    public DeleteDiseaseCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteDiseaseCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteDiseaseCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var disease = await _dbContext.Diseases
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (disease is null)
        {
            _logger.LogWarning("Disease with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Disease with id {request.Id} not found.");
        }

        disease.IsActive = false;
        disease.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted disease {DiseaseId}", disease.Id);
        return true;
    }
}
