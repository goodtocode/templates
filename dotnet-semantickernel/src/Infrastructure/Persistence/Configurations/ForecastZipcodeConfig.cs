using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;

namespace SemanticKernelMicroservice.Infrastructure.Persistence.Configurations;

public class ForecastZipcodeConfig : IEntityTypeConfiguration<WeatherForecastPostalCodeEntity>
{
    public void Configure(EntityTypeBuilder<WeatherForecastPostalCodeEntity> builder)
    {
        builder.ToTable("ForecastZipCodes");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
        builder
            .HasOne(x => x.WeatherForecast)
            .WithMany(x => x.ZipCodes);
    }
}