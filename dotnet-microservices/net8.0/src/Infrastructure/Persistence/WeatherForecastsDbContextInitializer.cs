﻿using Microsoft.Extensions.Logging;
using WeatherForecasts.Core.Domain.Forecasts.Entities;

namespace WeatherForecasts.Infrastructure.Persistence;

public class WeatherForecastsDbContextInitializer
{
    private readonly WeatherForecastsContext _context;
    private readonly ILogger<WeatherForecastsDbContextInitializer> _logger;

    public WeatherForecastsDbContextInitializer(ILogger<WeatherForecastsDbContextInitializer> logger,
        WeatherForecastsContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer()) await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Forecasts.Any())
        {
            for (var i = 0; i < 100; i++)
            {
                var forecastKey = Guid.NewGuid();
                var dateToday = DateTime.Now;
                var randomCode = GenerateRandomPostalCode();

                var weatherForecastAddValue =
                    ForecastValue.Create(forecastKey, dateToday.AddDays(-i).ToUniversalTime(),
                        Random.Shared.Next(-20, 55), new List<int>
                        {
                            randomCode, randomCode + 1
                        });
                var weatherForecast = new ForecastEntity(weatherForecastAddValue.Value);
                _context.Forecasts.Add(weatherForecast);
            }

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    public int GenerateRandomPostalCode()
    {
        var rnd = new Random();
        return rnd.Next(11111, 99999);
    }
}