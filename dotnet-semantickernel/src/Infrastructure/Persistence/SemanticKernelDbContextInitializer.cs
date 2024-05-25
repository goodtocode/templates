﻿using Microsoft.Extensions.Logging;
using SemanticKernel.Core.Domain.Forecasts.Entities;

namespace SemanticKernel.Infrastructure.Persistence;

public class SemanticKernelDbContextInitializer
{
    private readonly SemanticKernelContext _context;
    private readonly ILogger<SemanticKernelDbContextInitializer> _logger;

    public SemanticKernelDbContextInitializer(ILogger<SemanticKernelDbContextInitializer> logger,
        SemanticKernelContext context)
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
                var randomZipcode = GenerateRandomZipcode();

                var weatherForecastAddValue =
                    ForecastValue.Create(forecastKey, dateToday.AddDays(-i).ToUniversalTime(),
                        Random.Shared.Next(-20, 55), new List<int>
                        {
                            randomZipcode, randomZipcode + 1
                        });
                var weatherForecast = new Forecast(weatherForecastAddValue.Value);
                _context.Forecasts.Add(weatherForecast);
            }

            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }

    public int GenerateRandomZipcode()
    {
        var rnd = new Random();
        return rnd.Next(11111, 99999);
    }
}