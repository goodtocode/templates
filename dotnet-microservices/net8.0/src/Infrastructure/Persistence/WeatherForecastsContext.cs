using System.Reflection;
using WeatherForecasts.Core.Application.Common.Interfaces;
using WeatherForecasts.Core.Domain.Forecasts.Entities;
using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Infrastructure.Persistence;

public partial class WeatherForecastsContext : DbContext, IWeatherForecastsContext
{
    protected WeatherForecastsContext() { }

    public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options) : base(options) { }

    public DbSet<ForecastsView> ForecastViews => Set<ForecastsView>();

    public DbSet<ForecastEntity> Forecasts => Set<ForecastEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            x => x.Namespace == $"{GetType().Namespace}.Configurations");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}