using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace RythuMitraAI.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Common.Mapping.MappingProfile));

        // Register FluentValidation validators from this assembly. The API project should call AddControllers()/AddFluentValidation()
        services.AddValidatorsFromAssembly(typeof(Common.Mapping.MappingProfile).Assembly);

        // Register other application services, validators, handlers here

        return services;
    }
}
