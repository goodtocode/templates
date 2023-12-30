﻿namespace dotnet_semantickernel.Core.Application.ForecastCalculations.Queries.GetFahrenheitToCelsius;

public class
    GetFahrenheitToCelsiusCalculationConversionQueryValidator : AbstractValidator<
        GetFahrenheitToCelsiusCalculationConversionQuery>
{
    public GetFahrenheitToCelsiusCalculationConversionQueryValidator()
    {
        RuleFor(x => x.FahrenheitValue).NotNull();
    }
}