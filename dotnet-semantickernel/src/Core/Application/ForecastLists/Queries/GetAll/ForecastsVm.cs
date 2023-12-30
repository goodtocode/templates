namespace dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetAll;

public class ForecastsVm
{
    public IReadOnlyCollection<ForecastDto> Forecasts { get; init; }
}