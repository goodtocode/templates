using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Models;

namespace SemanticKernelMicroservice.Core.Application.Common.Interfaces;

public interface ISemanticKernelMicroserviceContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<Forecast> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}