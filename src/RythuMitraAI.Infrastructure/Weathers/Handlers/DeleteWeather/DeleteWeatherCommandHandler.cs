using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Weathers.Commands.DeleteWeather;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.DeleteWeather;

public sealed class DeleteWeatherCommandHandler : IRequestHandler<DeleteWeatherCommand, bool>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<DeleteWeatherCommandHandler> _logger;

    public DeleteWeatherCommandHandler(ApplicationDbContext dbContext, ILogger<DeleteWeatherCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(DeleteWeatherCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var weather = await _dbContext.Weathers
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (weather is null)
        {
            _logger.LogWarning("Weather with id {Id} not found for delete", request.Id);
            throw new NotFoundException($"Weather with id {request.Id} not found.");
        }

        if (!weather.IsActive)
        {
            _logger.LogWarning("Weather with id {Id} is already inactive", request.Id);
            throw new NotFoundException($"Weather with id {request.Id} is already inactive.");
        }

        weather.IsActive = false;
        weather.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Soft-deleted weather {WeatherId}", weather.Id);
        return true;
    }
}
