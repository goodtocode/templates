namespace dotnet_semantickernel.Core.Application.Forecasts.Queries.Get;

public class GetForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
{
    public GetForecastQueryValidator()
    {
        RuleFor(x => x.Key).NotEmpty();
    }
}