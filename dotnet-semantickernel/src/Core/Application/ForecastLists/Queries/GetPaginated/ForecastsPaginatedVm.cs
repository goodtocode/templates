namespace dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetPaginated;

public class ForecastsPaginatedVm
{
    public IReadOnlyCollection<ForecastPaginatedDto> Forecasts { get; init; }
}