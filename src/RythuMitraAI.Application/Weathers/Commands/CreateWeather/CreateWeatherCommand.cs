using MediatR;
using RythuMitraAI.Application.Weathers.DTOs;
using System;

namespace RythuMitraAI.Application.Weathers.Commands.CreateWeather;

public sealed class CreateWeatherCommand : IRequest<WeatherResponse>
{
    public CreateWeatherRequest Request { get; }

    public CreateWeatherCommand(CreateWeatherRequest request)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
    }
}
