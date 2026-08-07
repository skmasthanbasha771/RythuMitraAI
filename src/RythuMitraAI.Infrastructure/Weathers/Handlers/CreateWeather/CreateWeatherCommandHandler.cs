using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Weathers.Commands.CreateWeather;
using RythuMitraAI.Application.Weathers.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Weathers.Handlers.CreateWeather;

public sealed class CreateWeatherCommandHandler : IRequestHandler<CreateWeatherCommand, WeatherResponse>
{
    private readonly IGenericRepository<Weather> _weatherRepository;
    private readonly IGenericRepository<Farmer> _farmerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateWeatherCommandHandler> _logger;

    public CreateWeatherCommandHandler(
        IGenericRepository<Weather> weatherRepository,
        IGenericRepository<Farmer> farmerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateWeatherCommandHandler> logger)
    {
        _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        _farmerRepository = farmerRepository ?? throw new ArgumentNullException(nameof(farmerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<WeatherResponse> Handle(CreateWeatherCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var dto = request.Request;

        var farmer = await _farmerRepository.GetByIdAsync(dto.FarmerId, cancellationToken).ConfigureAwait(false);
        if (farmer is null)
            throw new InvalidOperationException($"Farmer with id {dto.FarmerId} does not exist.");

        var existing = await _weatherRepository.FindAsync(w => w.WeatherCode.ToLower() == dto.WeatherCode.Trim().ToLower(), cancellationToken).ConfigureAwait(false);
        if (existing.Any())
            throw new InvalidOperationException($"WeatherCode '{dto.WeatherCode}' already exists.");

        var weather = new Weather
        {
            WeatherCode = dto.WeatherCode.Trim(),
            FarmerId = dto.FarmerId,
            WeatherDate = dto.WeatherDate,
            Temperature = dto.Temperature,
            Humidity = dto.Humidity,
            Rainfall = dto.Rainfall,
            WindSpeed = dto.WindSpeed,
            WeatherCondition = dto.WeatherCondition.Trim(),
            IsActive = dto.IsActive
        };

        weather.SetCreated();

        await _weatherRepository.AddAsync(weather, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created weather {WeatherId} for farmer {FarmerId}", weather.Id, weather.FarmerId);

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
