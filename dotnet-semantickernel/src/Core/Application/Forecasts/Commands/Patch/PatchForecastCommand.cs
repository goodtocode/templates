using WeatherForecasts.Core.Application.Common.Exceptions;
using WeatherForecasts.Core.Application.Common.Interfaces;

namespace WeatherForecasts.Core.Application.Forecasts.Commands.Patch;

public class PatchForecastCommand : IRequest
{
    public Guid Key { get; set; }
    public DateTime? Date { get; set; }
    public int? TemperatureC { get; set; }
    public List<int>? Zipcodes { get; set; }
}

public class PatchWeatherForecastCommandHandler : IRequestHandler<PatchForecastCommand>
{
    private readonly IWeatherForecastsContext _context;

    public PatchWeatherForecastCommandHandler(IWeatherForecastsContext context)
    {
        _context = context;
    }

    public async Task Handle(PatchForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);

        if (weatherForecast == null)
            throw new NotFoundException();
        
        if (request.Date != null)
            weatherForecast.UpdateDate((DateTime) request.Date);

        if (request.TemperatureC != null)
            weatherForecast.UpdateTemperatureF((int) request.TemperatureC);

        if (request.Zipcodes != null)
            weatherForecast.UpdateZipcodes(request.Zipcodes);

        _context.Forecasts.Update(weatherForecast);
        await _context.SaveChangesAsync(CancellationToken.None);
    }
}