using FluentValidation;

namespace RythuMitraAI.Application.Farmers.Commands.CreateFarmer;

/// <summary>
/// Validator for <see cref="CreateFarmerCommand"/>.
/// </summary>
public sealed class CreateFarmerCommandValidator : AbstractValidator<CreateFarmerCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFarmerCommandValidator"/> class.
    /// </summary>
    public CreateFarmerCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create farmer request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.FarmerCode)
                .NotEmpty().WithMessage("FarmerCode is required.");

            RuleFor(x => x.Request.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Request.PhoneNumber)
                .MaximumLength(20).WithMessage("PhoneNumber must be at most 20 characters.");

            RuleFor(x => x.Request.LandArea)
                .GreaterThan(0).When(x => x.Request.LandArea.HasValue)
                .WithMessage("LandArea must be greater than zero when provided.");
        });
    }
}
