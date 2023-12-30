using System.Reflection;
using dotnet_semantickernel.Core.Application.Common.Interfaces;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;
using dotnet_semantickernel.Core.Domain.Forecasts.Models;

namespace dotnet_semantickernel.Infrastructure.Persistence;

public partial class dotnet_semantickernelContext : DbContext, Idotnet_semantickernelContext
{
    protected dotnet_semantickernelContext() { }

    public dotnet_semantickernelContext(DbContextOptions<dotnet_semantickernelContext> options) : base(options) { }

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