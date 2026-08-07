using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Weathers.DTOs;
using RythuMitraAI.Application.Weathers.Queries.GetWeatherById;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.GetWeatherById;

public sealed class GetWeatherByIdQueryHandler : IRequestHandler<GetWeatherByIdQuery, WeatherResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetWeatherByIdQueryHandler> _logger;

    public GetWeatherByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetWeatherByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<WeatherResponse> Handle(GetWeatherByIdQuery request, CancellationToken cancellationToken)
    {
        var weather = await _dbContext.Weathers
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (weather is null)
        {
            _logger.LogWarning("Weather with id {Id} not found", request.Id);
            throw new NotFoundException($"Weather with id {request.Id} not found.");
        }

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
