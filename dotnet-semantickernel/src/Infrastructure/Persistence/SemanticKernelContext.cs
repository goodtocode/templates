using System.Reflection;
using SemanticKernel.Core.Application.Common.Interfaces;
using SemanticKernel.Core.Domain.Forecasts.Entities;
using SemanticKernel.Core.Domain.Forecasts.Models;

namespace SemanticKernel.Infrastructure.Persistence;

public partial class SemanticKernelContext : DbContext, ISemanticKernelContext
{
    protected SemanticKernelContext() { }

    public SemanticKernelContext(DbContextOptions<SemanticKernelContext> options) : base(options) { }

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