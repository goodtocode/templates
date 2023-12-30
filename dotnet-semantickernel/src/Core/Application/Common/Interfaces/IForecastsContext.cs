using dotnet_semantickernel.Core.Domain.Forecasts.Entities;
using dotnet_semantickernel.Core.Domain.Forecasts.Models;

namespace dotnet_semantickernel.Core.Application.Common.Interfaces;

public interface Idotnet_semantickernelContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<Forecast> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}