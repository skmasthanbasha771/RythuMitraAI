using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Infrastructure.Persistence;
using RythuMitraAI.Infrastructure.Repositories;
using MediatR;
using RythuMitraAI.Infrastructure.Authentication.Handlers.Register;
using RythuMitraAI.Infrastructure.Authentication.Services;

namespace RythuMitraAI.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddMediatR(typeof(RegisterCommandHandler).Assembly);

        return services;
    }
}
