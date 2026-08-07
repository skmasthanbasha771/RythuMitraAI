using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Common.Models;
using RythuMitraAI.Application.Weathers.DTOs;
using RythuMitraAI.Application.Weathers.Queries.SearchWeather;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.SearchWeather;

public sealed class SearchWeatherQueryHandler : IRequestHandler<SearchWeatherQuery, PagedResponse<WeatherResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<SearchWeatherQueryHandler> _logger;

    public SearchWeatherQueryHandler(ApplicationDbContext dbContext, ILogger<SearchWeatherQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<PagedResponse<WeatherResponse>> Handle(SearchWeatherQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var query = _dbContext.Set<Domain.Entities.Weather>().AsNoTracking().Where(w => w.IsActive);

        if (!string.IsNullOrWhiteSpace(request.WeatherCode))
        {
            var code = request.WeatherCode.Trim().ToLower();
            query = query.Where(w => w.WeatherCode != null && EF.Functions.Like(w.WeatherCode.ToLower(), $"%{code}%"));
        }

        if (request.FarmerId.HasValue)
            query = query.Where(w => w.FarmerId == request.FarmerId.Value);

        if (request.WeatherDate.HasValue)
            query = query.Where(w => w.WeatherDate.Date == request.WeatherDate.Value.Date);

        if (!string.IsNullOrWhiteSpace(request.WeatherCondition))
        {
            var cond = request.WeatherCondition.Trim().ToLower();
            query = query.Where(w => w.WeatherCondition != null && EF.Functions.Like(w.WeatherCondition.ToLower(), $"%{cond}%"));
        }

        if (request.MinTemperature.HasValue)
            query = query.Where(w => w.Temperature >= request.MinTemperature.Value);
        if (request.MaxTemperature.HasValue)
            query = query.Where(w => w.Temperature <= request.MaxTemperature.Value);

        if (request.MinHumidity.HasValue)
            query = query.Where(w => w.Humidity >= request.MinHumidity.Value);
        if (request.MaxHumidity.HasValue)
            query = query.Where(w => w.Humidity <= request.MaxHumidity.Value);

        var total = await query.LongCountAsync(cancellationToken).ConfigureAwait(false);

        var items = await query
            .OrderByDescending(w => w.WeatherDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        var resultItems = items.Select(w => new WeatherResponse
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
        });

        _logger.LogDebug("Search returned {Count} weather records (total {Total})", resultItems.Count(), total);

        return new PagedResponse<WeatherResponse>(resultItems, pageNumber, pageSize, total);
    }
}
