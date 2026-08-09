using FluentValidation;

namespace RythuMitraAI.Application.Fertilizers.Commands.CreateFertilizer;

public sealed class CreateFertilizerCommandValidator : AbstractValidator<CreateFertilizerCommand>
{
    public CreateFertilizerCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create fertilizer request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.FertilizerCode).NotEmpty().WithMessage("FertilizerCode is required.").MaximumLength(50);
            RuleFor(x => x.Request.FertilizerName).NotEmpty().WithMessage("FertilizerName is required.").MaximumLength(150);
            RuleFor(x => x.Request.Brand).NotEmpty().WithMessage("Brand is required.").MaximumLength(100);
            RuleFor(x => x.Request.FertilizerType).NotEmpty().WithMessage("FertilizerType is required.").MaximumLength(100);
            RuleFor(x => x.Request.Nitrogen).GreaterThanOrEqualTo(0).WithMessage("Nitrogen must be non-negative.");
            RuleFor(x => x.Request.Phosphorus).GreaterThanOrEqualTo(0).WithMessage("Phosphorus must be non-negative.");
            RuleFor(x => x.Request.Potassium).GreaterThanOrEqualTo(0).WithMessage("Potassium must be non-negative.");
            RuleFor(x => x.Request.RecommendedCrop).NotEmpty().WithMessage("RecommendedCrop is required.").MaximumLength(100);
            RuleFor(x => x.Request.RecommendedSoil).NotEmpty().WithMessage("RecommendedSoil is required.").MaximumLength(100);
            RuleFor(x => x.Request.Description).MaximumLength(500);
        });
    }
}
