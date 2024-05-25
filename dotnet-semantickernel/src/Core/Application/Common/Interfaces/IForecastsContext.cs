using SemanticKernel.Core.Domain.Forecasts.Entities;
using SemanticKernel.Core.Domain.Forecasts.Models;

namespace SemanticKernel.Core.Application.Common.Interfaces;

public interface ISemanticKernelContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<Forecast> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}