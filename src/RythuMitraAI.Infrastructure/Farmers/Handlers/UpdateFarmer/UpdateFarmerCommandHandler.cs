using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Exceptions;
using RythuMitraAI.Application.Farmers.Commands.UpdateFarmer;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Infrastructure.Persistence;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.UpdateFarmer;

/// <summary>
/// Handles updating an existing farmer.
/// </summary>
public sealed class UpdateFarmerCommandHandler : IRequestHandler<UpdateFarmerCommand, FarmerResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UpdateFarmerCommandHandler> _logger;

    public UpdateFarmerCommandHandler(ApplicationDbContext dbContext, ILogger<UpdateFarmerCommandHandler> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<FarmerResponse> Handle(UpdateFarmerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var farmer = await _dbContext.Farmers
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (farmer is null)
        {
            _logger.LogWarning("Farmer with id {Id} not found for update", request.Id);
            throw new NotFoundException($"Farmer with id {request.Id} not found.");
        }

        // Update editable properties
        farmer.FirstName = request.FirstName?.Trim() ?? farmer.FirstName;
        farmer.LastName = request.LastName?.Trim() ?? farmer.LastName;
        farmer.PhoneNumber = request.PhoneNumber?.Trim() ?? farmer.PhoneNumber;
        farmer.Email = request.Email?.Trim() ?? farmer.Email;
        farmer.DateOfBirth = request.DateOfBirth ?? farmer.DateOfBirth;
        farmer.Gender = request.Gender?.Trim() ?? farmer.Gender;
        farmer.Address = request.Address?.Trim() ?? farmer.Address;
        farmer.Village = request.Village?.Trim() ?? farmer.Village;
        farmer.Mandal = request.Mandal?.Trim() ?? farmer.Mandal;
        farmer.District = request.District?.Trim() ?? farmer.District;
        farmer.State = request.State?.Trim() ?? farmer.State;
        farmer.Pincode = request.Pincode?.Trim() ?? farmer.Pincode;
        farmer.LandArea = request.LandArea ?? farmer.LandArea;
        farmer.LandUnit = request.LandUnit?.Trim() ?? farmer.LandUnit;
        farmer.ProfileImageUrl = request.ProfileImageUrl?.Trim() ?? farmer.ProfileImageUrl;
        farmer.IsActive = request.IsActive;

        // Set modified timestamp
        farmer.SetModified();

        await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Updated farmer {FarmerId}", farmer.Id);

        return new FarmerResponse
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
    }
}
