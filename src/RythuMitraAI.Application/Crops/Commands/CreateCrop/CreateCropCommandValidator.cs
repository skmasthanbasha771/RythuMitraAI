using FluentValidation;

namespace RythuMitraAI.Application.Crops.Commands.CreateCrop;

/// <summary>
/// Validator for <see cref="CreateCropCommand"/>.
/// </summary>
public sealed class CreateCropCommandValidator : AbstractValidator<CreateCropCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCropCommandValidator"/> class.
    /// </summary>
    public CreateCropCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create crop request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.CropCode)
                .NotEmpty().WithMessage("CropCode is required.")
                .MaximumLength(50).WithMessage("CropCode must be at most 50 characters.");

            RuleFor(x => x.Request.CropName)
                .NotEmpty().WithMessage("CropName is required.")
                .MaximumLength(150).WithMessage("CropName must be at most 150 characters.");

            RuleFor(x => x.Request.CropCategory)
                .NotEmpty().WithMessage("CropCategory is required.")
                .MaximumLength(100).WithMessage("CropCategory must be at most 100 characters.");

            RuleFor(x => x.Request.Season)
                .NotEmpty().WithMessage("Season is required.")
                .MaximumLength(100).WithMessage("Season must be at most 100 characters.");

            RuleFor(x => x.Request.SowingDate)
                .NotEmpty().WithMessage("SowingDate is required.");

            RuleFor(x => x.Request.Area)
                .GreaterThan(0).WithMessage("Area must be greater than zero.");

            RuleFor(x => x.Request.AreaUnit)
                .NotEmpty().WithMessage("AreaUnit is required.")
                .MaximumLength(50).WithMessage("AreaUnit must be at most 50 characters.");

            RuleFor(x => x.Request.FarmerId)
                .NotEmpty().WithMessage("FarmerId is required.");
        });
    }
}
