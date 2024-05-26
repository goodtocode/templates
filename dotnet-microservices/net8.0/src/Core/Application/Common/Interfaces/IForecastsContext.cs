using WeatherForecasts.Core.Domain.Forecasts.Entities;
using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Core.Application.Common.Interfaces;

public interface IWeatherForecastsContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<ForecastEntity> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}