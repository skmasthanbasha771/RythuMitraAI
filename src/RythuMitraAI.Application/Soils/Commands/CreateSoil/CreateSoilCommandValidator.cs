using FluentValidation;

namespace RythuMitraAI.Application.Soils.Commands.CreateSoil;

public sealed class CreateSoilCommandValidator : AbstractValidator<CreateSoilCommand>
{
    public CreateSoilCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create soil request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.SoilCode).NotEmpty().WithMessage("SoilCode is required.").MaximumLength(50);
            RuleFor(x => x.Request.FarmerId).NotEmpty().WithMessage("FarmerId is required.");
            RuleFor(x => x.Request.PH).GreaterThanOrEqualTo(0).WithMessage("PH must be non-negative.");
            RuleFor(x => x.Request.Moisture).GreaterThanOrEqualTo(0).WithMessage("Moisture must be non-negative.");
            RuleFor(x => x.Request.Nitrogen).GreaterThanOrEqualTo(0).WithMessage("Nitrogen must be non-negative.");
            RuleFor(x => x.Request.Phosphorus).GreaterThanOrEqualTo(0).WithMessage("Phosphorus must be non-negative.");
            RuleFor(x => x.Request.Potassium).GreaterThanOrEqualTo(0).WithMessage("Potassium must be non-negative.");
            RuleFor(x => x.Request.OrganicCarbon).GreaterThanOrEqualTo(0).WithMessage("OrganicCarbon must be non-negative.");
            RuleFor(x => x.Request.TestDate).NotEmpty().WithMessage("TestDate is required.");
            RuleFor(x => x.Request.Remarks).MaximumLength(500);
        });
    }
}
