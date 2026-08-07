using System;
using MediatR;

namespace RythuMitraAI.Application.Weathers.Commands.DeleteWeather;

/// <summary>
/// Command to soft-delete a weather record by id.
/// </summary>
public sealed class DeleteWeatherCommand : IRequest<bool>
{
    public Guid Id { get; }

    public DeleteWeatherCommand(Guid id)
    {
        Id = id;
    }
}
