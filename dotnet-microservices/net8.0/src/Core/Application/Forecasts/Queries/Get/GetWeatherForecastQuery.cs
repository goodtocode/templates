using WeatherForecasts.Core.Application.Common.Exceptions;
using WeatherForecasts.Core.Application.Common.Interfaces;
using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Core.Application.Forecasts.Queries.Get;

public class GetWeatherForecastQuery : IRequest<ForecastVm>
{
    public Guid Key { get; set; }
}

public class GetForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, ForecastVm>
{
    private readonly IWeatherForecastsContext _context;
    private readonly IMapper _mapper;

    public GetForecastQueryHandler(IWeatherForecastsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ForecastVm> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var forecast = await _context.ForecastViews.FindAsync(request.Key);
        GuardAgainstForecastNotFound(forecast);

        var weatherForecast = _mapper.Map<ForecastVm>(forecast);
        weatherForecast.TemperatureC = (forecast.TemperatureF - 32) * 5 / 9;
        
        return weatherForecast;
    }

    private static void GuardAgainstForecastNotFound(ForecastsView? forecast)
    {
        if (forecast == null)
            throw new NotFoundException("Forecast Not Found");
    }
}