using FluentValidation;

namespace RythuMitraAI.Application.Diseases.Commands.CreateDisease;

/// <summary>
/// Validator for <see cref="CreateDiseaseCommand"/>.
/// </summary>
public sealed class CreateDiseaseCommandValidator : AbstractValidator<CreateDiseaseCommand>
{
    public CreateDiseaseCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create disease request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.DiseaseCode)
                .NotEmpty().WithMessage("DiseaseCode is required.")
                .MaximumLength(50).WithMessage("DiseaseCode must be at most 50 characters.");

            RuleFor(x => x.Request.DiseaseName)
                .NotEmpty().WithMessage("DiseaseName is required.")
                .MaximumLength(150).WithMessage("DiseaseName must be at most 150 characters.");

            RuleFor(x => x.Request.CropType)
                .NotEmpty().WithMessage("CropType is required.")
                .MaximumLength(100).WithMessage("CropType must be at most 100 characters.");

            RuleFor(x => x.Request.Symptoms)
                .NotEmpty().WithMessage("Symptoms is required.")
                .MaximumLength(1000).WithMessage("Symptoms must be at most 1000 characters.");

            RuleFor(x => x.Request.Causes)
                .NotEmpty().WithMessage("Causes is required.")
                .MaximumLength(1000).WithMessage("Causes must be at most 1000 characters.");

            RuleFor(x => x.Request.Treatment)
                .NotEmpty().WithMessage("Treatment is required.")
                .MaximumLength(1000).WithMessage("Treatment must be at most 1000 characters.");

            RuleFor(x => x.Request.Prevention)
                .NotEmpty().WithMessage("Prevention is required.")
                .MaximumLength(1000).WithMessage("Prevention must be at most 1000 characters.");

            RuleFor(x => x.Request.Severity)
                .NotEmpty().WithMessage("Severity is required.")
                .MaximumLength(50).WithMessage("Severity must be at most 50 characters.");
        });
    }
}
