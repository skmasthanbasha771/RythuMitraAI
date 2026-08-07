using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Weathers.DTOs;
using RythuMitraAI.Application.Weathers.Queries.GetAllWeather;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.GetAllWeather;

public sealed class GetAllWeatherQueryHandler : IRequestHandler<GetAllWeatherQuery, IEnumerable<WeatherResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetAllWeatherQueryHandler> _logger;

    public GetAllWeatherQueryHandler(ApplicationDbContext dbContext, ILogger<GetAllWeatherQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<WeatherResponse>> Handle(GetAllWeatherQuery request, CancellationToken cancellationToken)
    {
        var weathers = await _dbContext.Weathers
            .AsNoTracking()
            .Where(w => w.IsActive)
            .OrderByDescending(w => w.WeatherDate)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var result = weathers.Select(w => new WeatherResponse
        {
            Id = w.Id,
            WeatherCode = w.WeatherCode,
            FarmerId = w.FarmerId,
            WeatherDate = w.WeatherDate,
            Temperature = w.Temperature,
            Humidity = w.Humidity,
            Rainfall = w.Rainfall,
            WindSpeed = w.WindSpeed,
            WeatherCondition = w.WeatherCondition,
            IsActive = w.IsActive,
            CreatedAtUtc = w.CreatedAt
        }).ToList();

        _logger.LogDebug("Retrieved {Count} active weather records", result.Count);

        return result;
    }
}
