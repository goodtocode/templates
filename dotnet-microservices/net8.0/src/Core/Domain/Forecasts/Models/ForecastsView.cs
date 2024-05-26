namespace WeatherForecasts.Core.Domain.Forecasts.Models;

public class ForecastsView
{
    public Guid Key { get; set; }

    public int TemperatureF { get; set; }

    public string PostalCodesSearch { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public DateTime ForecastDate { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }
}    
