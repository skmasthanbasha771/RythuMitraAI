using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RythuMitraAI.Application.Farmers.Commands.CreateFarmer;
using RythuMitraAI.Application.Farmers.DTOs;
using RythuMitraAI.Application.Interfaces;
using RythuMitraAI.Domain.Entities;

namespace RythuMitraAI.Infrastructure.Farmers.Handlers.CreateFarmer;

/// <summary>
/// Handles <see cref="CreateFarmerCommand"/> to create and persist a Farmer entity.
/// </summary>
public sealed class CreateFarmerCommandHandler : IRequestHandler<CreateFarmerCommand, CreateFarmerResponse>
{
    private readonly IGenericRepository<Farmer> _farmerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateFarmerCommandHandler> _logger;

    public CreateFarmerCommandHandler(
        IGenericRepository<Farmer> farmerRepository,
        IUnitOfWork unitOfWork,
        ILogger<CreateFarmerCommandHandler> logger)
    {
        _farmerRepository = farmerRepository ?? throw new ArgumentNullException(nameof(farmerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateFarmerResponse> Handle(CreateFarmerCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        var dto = request.Request;
        cancellationToken.ThrowIfCancellationRequested();

        var farmer = new Farmer
        {
            FarmerCode = dto.FarmerCode?.Trim() ?? string.Empty,
            FirstName = dto.FirstName?.Trim() ?? string.Empty,
            LastName = dto.LastName?.Trim() ?? string.Empty,
            PhoneNumber = dto.PhoneNumber?.Trim(),
            Email = dto.Email?.Trim() ?? string.Empty,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender?.Trim(),
            Address = dto.Address?.Trim(),
            Village = dto.Village?.Trim(),
            Mandal = dto.Mandal?.Trim(),
            District = dto.District?.Trim(),
            State = dto.State?.Trim(),
            Pincode = dto.Pincode?.Trim(),
            LandArea = dto.LandArea,
            LandUnit = dto.LandUnit?.Trim(),
            //ProfileImageUrl = dto.ProfileImageUrl?.Trim(),
            IsActive = dto.IsActive
        };

        // Set auditing information
        farmer.SetCreated();

        await _farmerRepository.AddAsync(farmer, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.LogInformation("Created farmer {FarmerId} with code {FarmerCode}", farmer.Id, farmer.FarmerCode);

        return new CreateFarmerResponse
        {
            Id = farmer.Id,
            FarmerCode = farmer.FarmerCode,
            FirstName = farmer.FirstName,
            LastName = farmer.LastName,
            Email = farmer.Email,
            PhoneNumber = farmer.PhoneNumber,
            IsActive = farmer.IsActive,
            CreatedAtUtc = farmer.CreatedAt,
            Message = "Farmer created successfully."
        };
    }
}
