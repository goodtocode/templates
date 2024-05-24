using System.Collections.Concurrent;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using WeatherForecasts.Core.Application;
using WeatherForecasts.Core.Application.Common.Exceptions;
using WeatherForecasts.Infrastructure;
using WeatherForecasts.Infrastructure.Persistence;
using Table = Respawn.Graph.Table;

namespace WeatherForecasts.Specs.Application.Integration;

[SetUpFixture]
public class TestBase
{
    public enum CommandResponseType
    {
        Successful,
        BadRequest,
        NotFound,
        Error
    }

    private static IServiceScopeFactory _scopeFactory = null!;
    private static Respawner _checkpoint = null!;
    public static CommandResponseType _responseType;
    public static ValidationResult ValidationResponse = new();
    public static IDictionary<string, string[]> CommandErrors = new ConcurrentDictionary<string, string[]>();
    private WeatherForecastsDbContextInitializer _contextInitializer;
    public static IConfiguration Configuration { get; private set; }

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.test.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddApplicationServices();
        services.AddInfrastructureServices(Configuration);
        var sp = services.BuildServiceProvider();

        _scopeFactory = services
            .BuildServiceProvider()
            .GetRequiredService<IServiceScopeFactory>();

        _contextInitializer = services
            .BuildServiceProvider()
            .GetRequiredService<WeatherForecastsDbContextInitializer>();

        await _contextInitializer.InitialiseAsync();
        await _contextInitializer.SeedAsync();

        _checkpoint = Respawner.CreateAsync(Configuration.GetConnectionString("DefaultConnection")!,
            new RespawnerOptions
            {
                TablesToIgnore = new Table[] {"__EFMigrationsHistory"}
            }).GetAwaiter().GetResult();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }
    
    public static async Task ResetState()
    {
        await _checkpoint.ResetAsync(Configuration.GetConnectionString("DefaultConnection")!);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<WeatherForecastsContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<WeatherForecastsContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static CommandResponseType CreateCommandResponseType
        (Exception e)
    {
        switch (e)
        {
            case ValidationException validationException:
                CommandErrors = validationException.Errors;
                _responseType = CommandResponseType.BadRequest;
                break;
            case NotFoundException notFoundException:
                _responseType = CommandResponseType.NotFound;
                break;
            default:
                _responseType = CommandResponseType.Error;
                break;
        }

        return _responseType;
    }

    public static void HandleHasResponseType(string response)
    {
        switch (response)
        {
            case "Success":
                _responseType.Should().Be(CommandResponseType.Successful);
                break;
            case "BadRequest":
                _responseType.Should().Be(CommandResponseType.BadRequest);
                break;
            case "NotFound":
                _responseType.Should().Be(CommandResponseType.NotFound);
                break;
        }
    }

    public static void HandleExpectedValidationErrorsAssertions(string expectedErrors)
    {
        if (string.IsNullOrWhiteSpace(expectedErrors)) return;

        var expectedErrorsCollection = expectedErrors.Split(",");

        foreach (var field in expectedErrorsCollection)
        {
            var hasCommandValidatorErrors = ValidationResponse.Errors.Any(x => x.PropertyName == field.Trim());
            var hasCommandErrors = CommandErrors.Any(x => x.Key == field.Trim());
            var hasErrorMatch = hasCommandErrors || hasCommandValidatorErrors;
            hasErrorMatch.Should().BeTrue();
        }
    }
    public CommandResponseType HandleAssignResponseType
        (Exception e)
    {
        switch (e)
        {
            case ValidationException validationException:
                CommandErrors = validationException.Errors;
                _responseType = CommandResponseType.BadRequest;
                break;
            case NotFoundException notFoundException:
                _responseType = CommandResponseType.NotFound;
                break;
            default:
                _responseType = CommandResponseType.Error;
                break;
        }

        return _responseType;
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}