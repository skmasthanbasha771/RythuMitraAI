using System;

namespace RythuMitraAI.Application.Exceptions;

/// <summary>
/// Exception representing a not found resource.
/// Throw this from application handlers when an entity cannot be located.
/// </summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}
