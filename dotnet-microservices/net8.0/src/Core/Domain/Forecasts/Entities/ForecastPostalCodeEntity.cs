namespace WeatherForecasts.Core.Domain.Forecasts.Entities;

public class ForecastPostalCodeEntity : Common.Entity
{
    protected ForecastPostalCodeEntity() { }

    public ForecastPostalCodeEntity(int postalCode, ForecastEntity weatherForecast)
    {
        PostalCode = postalCode;
        WeatherForecast = weatherForecast;
    }

    public virtual int PostalCode { get; private set; }

    public virtual ForecastEntity WeatherForecast { get; private set; }
}