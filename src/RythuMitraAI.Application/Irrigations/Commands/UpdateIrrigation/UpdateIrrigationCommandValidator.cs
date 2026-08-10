using FluentValidation;

namespace RythuMitraAI.Application.Irrigations.Commands.UpdateIrrigation;

/// <summary>
/// Validator for <see cref="UpdateIrrigationCommand"/>.
/// </summary>
public sealed class UpdateIrrigationCommandValidator : AbstractValidator<UpdateIrrigationCommand>
{
    public UpdateIrrigationCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Update irrigation request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.IrrigationCode)
                .NotEmpty().WithMessage("IrrigationCode is required.")
                .MaximumLength(50).WithMessage("IrrigationCode must be at most 50 characters.");

            RuleFor(x => x.Request.FarmerId)
                .NotEmpty().WithMessage("FarmerId is required.");

            RuleFor(x => x.Request.CropId)
                .NotEmpty().WithMessage("CropId is required.");

            RuleFor(x => x.Request.IrrigationType)
                .NotEmpty().WithMessage("IrrigationType is required.")
                .MaximumLength(100).WithMessage("IrrigationType must be at most 100 characters.");

            RuleFor(x => x.Request.WaterSource)
                .NotEmpty().WithMessage("WaterSource is required.")
                .MaximumLength(100).WithMessage("WaterSource must be at most 100 characters.");

            RuleFor(x => x.Request.IrrigationDate)
                .NotEmpty().WithMessage("IrrigationDate is required.");

            RuleFor(x => x.Request.DurationInMinutes)
                .GreaterThanOrEqualTo(0).WithMessage("DurationInMinutes must be non-negative.");

            RuleFor(x => x.Request.WaterQuantity)
                .GreaterThan(0).WithMessage("WaterQuantity must be greater than zero.");

            RuleFor(x => x.Request.WaterUnit)
                .NotEmpty().WithMessage("WaterUnit is required.")
                .MaximumLength(50).WithMessage("WaterUnit must be at most 50 characters.");

            RuleFor(x => x.Request.Remarks)
                .MaximumLength(500).WithMessage("Remarks must be at most 500 characters.");
        });
    }
}
