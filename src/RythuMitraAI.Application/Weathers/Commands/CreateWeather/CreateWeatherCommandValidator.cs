using FluentValidation;

namespace RythuMitraAI.Application.Weathers.Commands.CreateWeather;

public sealed class CreateWeatherCommandValidator : AbstractValidator<CreateWeatherCommand>
{
    public CreateWeatherCommandValidator()
    {
        RuleFor(x => x.Request).NotNull().WithMessage("Create weather request is required.");

        When(x => x.Request != null, () =>
        {
            RuleFor(x => x.Request.WeatherCode).NotEmpty().WithMessage("WeatherCode is required.").MaximumLength(50);
            RuleFor(x => x.Request.FarmerId).NotEmpty().WithMessage("FarmerId is required.");
            RuleFor(x => x.Request.WeatherDate).NotEmpty().WithMessage("WeatherDate is required.");
            RuleFor(x => x.Request.Temperature).GreaterThanOrEqualTo(-100).WithMessage("Temperature must be a valid value.");
            RuleFor(x => x.Request.Humidity).GreaterThanOrEqualTo(0).WithMessage("Humidity must be non-negative.");
            RuleFor(x => x.Request.Rainfall).GreaterThanOrEqualTo(0).WithMessage("Rainfall must be non-negative.");
            RuleFor(x => x.Request.WindSpeed).GreaterThanOrEqualTo(0).WithMessage("WindSpeed must be non-negative.");
            RuleFor(x => x.Request.WeatherCondition).NotEmpty().WithMessage("WeatherCondition is required.").MaximumLength(100);
        });
    }
}
