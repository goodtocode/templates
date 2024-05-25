using AutoMapper.QueryableExtensions;
using SemanticKernel.Core.Application.Common.Interfaces;
using SemanticKernel.Core.Application.Common.Mappings;
using SemanticKernel.Core.Application.Common.Models;

namespace SemanticKernel.Core.Application.ForecastLists.Queries.GetPaginated;

public class GetForecastsPaginatedQuery : IRequest<PaginatedList<ForecastPaginatedDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetSemanticKernelPaginatedQueryHandler : IRequestHandler<GetForecastsPaginatedQuery,
        PaginatedList<ForecastPaginatedDto>>
{
    private readonly ISemanticKernelContext _context;
    private readonly IMapper _mapper;

    public GetSemanticKernelPaginatedQueryHandler(ISemanticKernelContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ForecastPaginatedDto>> Handle(GetForecastsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedSemanticKernel = await _context.ForecastViews
            .AsNoTracking()
            .OrderByDescending(x => x.ForecastDate)
            .ProjectTo<ForecastPaginatedDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        foreach (var item in paginatedSemanticKernel.Items) item.TemperatureC = (item.TemperatureF - 32) * 5 / 9;

        return paginatedSemanticKernel;
    }
}