using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Weathers.Commands.UpdateWeather;
using RythuMitraAI.Application.Weathers.DTOs;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.UpdateWeather;

public sealed class UpdateWeatherCommandHandler : IRequestHandler<UpdateWeatherCommand, WeatherResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateWeatherCommandHandler> _logger;

    public UpdateWeatherCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateWeatherCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<WeatherResponse> Handle(UpdateWeatherCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var weather = await _dbContext.Weathers
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (weather is null)
        {
            _logger.LogWarning("Weather with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Weather with id {request.Id} not found.");
        }

        // Validate farmer exists
        var farmer = await _dbContext.Farmers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Request.FarmerId, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {request.Request.FarmerId} does not exist.");

        // Update editable fields only
        weather.FarmerId = request.Request.FarmerId;
        weather.WeatherDate = request.Request.WeatherDate;
        weather.Temperature = request.Request.Temperature;
        weather.Humidity = request.Request.Humidity;
        weather.Rainfall = request.Request.Rainfall;
        weather.WindSpeed = request.Request.WindSpeed;
        weather.WeatherCondition = request.Request.WeatherCondition.Trim();
        weather.IsActive = request.Request.IsActive;

        weather.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated weather {WeatherId}", weather.Id);

        return new WeatherResponse
        {
            Id = weather.Id,
            WeatherCode = weather.WeatherCode,
            FarmerId = weather.FarmerId,
            WeatherDate = weather.WeatherDate,
            Temperature = weather.Temperature,
            Humidity = weather.Humidity,
            Rainfall = weather.Rainfall,
            WindSpeed = weather.WindSpeed,
            WeatherCondition = weather.WeatherCondition,
            IsActive = weather.IsActive,
            CreatedAtUtc = weather.CreatedAt
        };
    }
}
