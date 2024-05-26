namespace WeatherForecasts.Core.Application.Forecasts.Commands.Add;

public class AddForecastCommandValidator : AbstractValidator<AddForecastCommand>
{
    public AddForecastCommandValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
        RuleFor(x => x.TemperatureF).NotEmpty();
        RuleFor(x => x.PostalCodes).NotEmpty();
        RuleFor(x => x.Date).NotEmpty().GreaterThan(DateTime.MinValue);
    }
}