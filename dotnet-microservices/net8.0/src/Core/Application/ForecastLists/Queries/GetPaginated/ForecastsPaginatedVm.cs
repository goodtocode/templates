﻿namespace WeatherForecasts.Core.Application.ForecastLists.Queries.GetPaginated;

public class ForecastsPaginatedVm
{
    public IReadOnlyCollection<ForecastPaginatedDto> Forecasts { get; init; }
}