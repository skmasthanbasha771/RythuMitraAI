using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Application.Farmers.Queries.GetFarmerById;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.GetFarmerById;

/// <summary>
/// Handles retrieving a farmer by identifier.
/// </summary>
public sealed class GetFarmerByIdQueryHandler : IRequestHandler<GetFarmerByIdQuery, FarmerResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<GetFarmerByIdQueryHandler> _logger;

    public GetFarmerByIdQueryHandler(ApplicationDbContext dbContext, ILogger<GetFarmerByIdQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<FarmerResponse> Handle(GetFarmerByIdQuery request, CancellationToken cancellationToken)
    {
        var farmer = await _dbContext.Farmers
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
        {
            _logger.LogWarning("Farmer with id {Id} not found", request.Id);
            throw new NotFoundException($"Farmer with id {request.Id} not found.");
        }

        var response = new FarmerResponse
        {
            Id = farmer.Id,
            FarmerCode = farmer.FarmerCode,
            FirstName = farmer.FirstName,
            LastName = farmer.LastName,
            PhoneNumber = farmer.PhoneNumber,
            Email = farmer.Email,
            Village = farmer.Village,
            District = farmer.District,
            State = farmer.State,
            LandArea = farmer.LandArea,
            LandUnit = farmer.LandUnit,
            IsActive = farmer.IsActive,
            CreatedAtUtc = farmer.CreatedAt,
            UpdatedAtUtc = farmer.ModifiedAt
        };

        return response;
    }
}
