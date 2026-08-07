using System;
using MediatR;
using RythuMitraAI.Application.Weathers.DTOs;

namespace RythuMitraAI.Application.Weathers.Commands.UpdateWeather;

public sealed class UpdateWeatherCommand : IRequest<RythuMitraAI.Application.Weathers.DTOs.WeatherResponse>
{
    public Guid Id { get; }
    public UpdateWeatherRequest Request { get; }

    public UpdateWeatherCommand(Guid id, UpdateWeatherRequest request)
    {
        Id = id;
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
