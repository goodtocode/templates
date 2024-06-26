﻿using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Infrastructure.Persistence.Configurations;

public class ForecastViewConfig : IEntityTypeConfiguration<ForecastsView>
{
    public void Configure(EntityTypeBuilder<ForecastsView> builder)
    {
        builder.ToView("ForecastsView");
        builder.HasKey(x=>x.Key);
        builder.Property(e => e.Key);
        builder.Property(e => e.TemperatureF);
        builder.Property(e => e.DateAdded);
        builder.Property(e => e.DateUpdated);
        builder.Property(e => e.ForecastDate);
        builder.Property(e => e.Summary).HasMaxLength(40);
        builder.Property(e => e.PostalCodesSearch).HasMaxLength(50);
    }
}