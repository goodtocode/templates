using dotnet_semantickernel.Core.Application.Common.Exceptions;
using dotnet_semantickernel.Core.Application.Common.Interfaces;
using dotnet_semantickernel.Core.Domain.Forecasts.Models;

namespace dotnet_semantickernel.Core.Application.Forecasts.Queries.Get;

public class GetWeatherForecastQuery : IRequest<ForecastVm>
{
    public Guid Key { get; set; }
}

public class GetForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, ForecastVm>
{
    private readonly Idotnet_semantickernelContext _context;
    private readonly IMapper _mapper;

    public GetForecastQueryHandler(Idotnet_semantickernelContext context, IMapper mapper)
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