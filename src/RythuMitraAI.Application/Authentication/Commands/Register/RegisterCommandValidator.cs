using FluentValidation;

namespace RythuMitraAI.Application.Authentication.Commands.Register;

/// <summary>
/// Validator for <see cref="RegisterCommand"/> using FluentValidation.
/// Validates the nested RegisterRequest properties according to business rules.
/// </summary>
public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandValidator"/> class.
    /// </summary>
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Request)
            .NotNull().WithMessage("Registration request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.Request.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.Request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.Request.ConfirmPassword)
                .Equal(x => x.Request.Password).WithMessage("Passwords do not match.");
        });
    }
}
