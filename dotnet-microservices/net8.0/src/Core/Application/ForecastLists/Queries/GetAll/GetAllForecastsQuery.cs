using AutoMapper.QueryableExtensions;
using WeatherForecasts.Core.Application.Common.Interfaces;

namespace WeatherForecasts.Core.Application.ForecastLists.Queries.GetAll;

public class GetAllForecastsQuery : IRequest<ForecastsVm>
{
    public string ZipcodeFilter { get; init; }
}

public class GetAllWeatherForecastQueryHandler : IRequestHandler<GetAllForecastsQuery, ForecastsVm>
{
    private readonly IWeatherForecastsContext _context;
    private readonly IMapper _mapper;

    public GetAllWeatherForecastQueryHandler(IWeatherForecastsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ForecastsVm> Handle(GetAllForecastsQuery request,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.ZipcodeFilter))
            return await GetFilteredWeatherForecasts(request, cancellationToken);

        var vm = await GetAllWeatherForecasts(cancellationToken);
        PopulateCalculationsF(vm);
        return vm;
    }

    private static void PopulateCalculationsF(ForecastsVm vm)
    {
        foreach (var weatherForecast in vm.Forecasts)
            weatherForecast.TemperatureF = 32 + (int) (weatherForecast.TemperatureC / 0.5556);
    }

    private async Task<ForecastsVm> GetAllWeatherForecasts(CancellationToken cancellationToken)
    {
        ForecastsVm vm;

        vm = new ForecastsVm
        {
            Forecasts = await _context.ForecastViews
                .OrderByDescending(x => x.ForecastDate)
                .ProjectTo<ForecastDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
        return vm;
    }

    private async Task<ForecastsVm> GetFilteredWeatherForecasts(GetAllForecastsQuery request,
        CancellationToken cancellationToken)
    {
        ForecastsVm vm;
        vm = new ForecastsVm
        {
            Forecasts = await _context.ForecastViews
                .Where(x => x.ZipCodesSearch.Contains(request.ZipcodeFilter))
                .OrderByDescending(x => x.ForecastDate)
                .ProjectTo<ForecastDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
        return vm;
    }
}