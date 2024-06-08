using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;

namespace SemanticKernelMicroservice.Core.Application.Abstractions;

public interface ISemanticKernelMicroserviceContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<Forecast> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}