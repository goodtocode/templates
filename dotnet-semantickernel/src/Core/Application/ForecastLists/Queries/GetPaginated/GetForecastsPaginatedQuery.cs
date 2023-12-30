using AutoMapper.QueryableExtensions;
using dotnet_semantickernel.Core.Application.Common.Interfaces;
using dotnet_semantickernel.Core.Application.Common.Mappings;
using dotnet_semantickernel.Core.Application.Common.Models;

namespace dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetPaginated;

public class GetForecastsPaginatedQuery : IRequest<PaginatedList<ForecastPaginatedDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    Getdotnet_semantickernelPaginatedQueryHandler : IRequestHandler<GetForecastsPaginatedQuery,
        PaginatedList<ForecastPaginatedDto>>
{
    private readonly Idotnet_semantickernelContext _context;
    private readonly IMapper _mapper;

    public Getdotnet_semantickernelPaginatedQueryHandler(Idotnet_semantickernelContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ForecastPaginatedDto>> Handle(GetForecastsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var paginateddotnet_semantickernel = await _context.ForecastViews
            .AsNoTracking()
            .OrderByDescending(x => x.ForecastDate)
            .ProjectTo<ForecastPaginatedDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        foreach (var item in paginateddotnet_semantickernel.Items) item.TemperatureC = (item.TemperatureF - 32) * 5 / 9;

        return paginateddotnet_semantickernel;
    }
}