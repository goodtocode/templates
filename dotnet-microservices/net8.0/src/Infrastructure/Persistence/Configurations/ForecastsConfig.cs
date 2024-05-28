using WeatherForecasts.Core.Domain.Forecasts.Entities;

namespace WeatherForecasts.Infrastructure.Persistence.Configurations;

public class ForecastsConfig : IEntityTypeConfiguration<ForecastEntity>
{
    public void Configure(EntityTypeBuilder<ForecastEntity> builder)
    {
        builder.ToTable("Forecasts");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
        builder.Property(x => x.TemperatureF);
        builder.Property(x => x.Summary).HasMaxLength(40);
        builder.Property(x => x.PostalCodesSearch).HasMaxLength(50);
        builder.Property(x => x.DateAdded);
        builder.Property(x => x.DateUpdated);
        builder
            .HasMany(x => x.PostalCodes)
            .WithOne(x => x.WeatherForecast);
    }
}