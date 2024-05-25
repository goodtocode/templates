using System.Reflection;
using SemanticKernelMicroservice.Core.Application.Common.Interfaces;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Models;

namespace SemanticKernelMicroservice.Infrastructure.Persistence;

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}