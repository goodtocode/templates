using System.Reflection;
using SemanticKernelMicroservice.Core.Application.Abstractions;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;

namespace SemanticKernelMicroservice.Infrastructure.SqlServer.Persistence;

public partial class SemanticKernelMicroserviceContext : DbContext, ISemanticKernelMicroserviceContext
{
    protected SemanticKernelMicroserviceContext() { }

    public SemanticKernelMicroserviceContext(DbContextOptions<SemanticKernelMicroserviceContext> options) : base(options) { }

    public DbSet<ForecastsView> ForecastViews => Set<ForecastsView>();

    public DbSet<Forecast> Forecasts => Set<Forecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            x => x.Namespace == $"{GetType().Namespace}.Configurations");
    }
}