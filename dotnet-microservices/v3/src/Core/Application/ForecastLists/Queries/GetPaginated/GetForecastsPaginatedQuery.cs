using AutoMapper.QueryableExtensions;
using WeatherForecasts.Core.Application.Common.Interfaces;
using WeatherForecasts.Core.Application.Common.Mappings;
using WeatherForecasts.Core.Application.Common.Models;

namespace WeatherForecasts.Core.Application.ForecastLists.Queries.GetPaginated;

public class GetForecastsPaginatedQuery : IRequest<PaginatedList<ForecastPaginatedDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetWeatherForecastsPaginatedQueryHandler : IRequestHandler<GetForecastsPaginatedQuery,
        PaginatedList<ForecastPaginatedDto>>
{
    private readonly IWeatherForecastsContext _context;
    private readonly IMapper _mapper;

    public GetWeatherForecastsPaginatedQueryHandler(IWeatherForecastsContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ForecastPaginatedDto>> Handle(GetForecastsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedWeatherForecasts = await _context.ForecastViews
            .AsNoTracking()
            .OrderByDescending(x => x.ForecastDate)
            .ProjectTo<ForecastPaginatedDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        foreach (var item in paginatedWeatherForecasts.Items) item.TemperatureC = (item.TemperatureF - 32) * 5 / 9;

        return paginatedWeatherForecasts;
    }
}