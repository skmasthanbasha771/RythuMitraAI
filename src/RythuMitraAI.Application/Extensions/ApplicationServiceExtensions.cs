using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace RythuMitraAI.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Common.Mapping.MappingProfile));

        // Register validators in the API project using FluentValidation.AspNetCore (keep Application free of Web dependencies)

        // Register other application services, validators, handlers here

        return services;
    }
}
