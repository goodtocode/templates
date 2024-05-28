using WeatherForecasts.Core.Domain.Forecasts.Entities;

namespace WeatherForecasts.Infrastructure.Persistence.Configurations;

public class ForecastPostalCodeConfig : IEntityTypeConfiguration<ForecastPostalCodeEntity>
{
    public void Configure(EntityTypeBuilder<ForecastPostalCodeEntity> builder)
    {
        builder.ToTable("ForecastPostalCodes");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
        builder
            .HasOne(x => x.WeatherForecast)
            .WithMany(x => x.PostalCodes);
    }
}