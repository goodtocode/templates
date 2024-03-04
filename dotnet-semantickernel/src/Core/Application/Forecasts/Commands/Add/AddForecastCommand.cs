using FluentValidation.Results;
using dotnet_semantickernel.Core.Application.Common.Interfaces;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;
using ValidationException = dotnet_semantickernel.Core.Application.Common.Exceptions.ValidationException;

namespace dotnet_semantickernel.Core.Application.Forecasts.Commands.Add;

public class AddForecastCommand : IRequest
{
    public Guid Key { get; set; }

    public DateTime Date { get; set; }

    public int? TemperatureF { get; set; }

    public List<int> Zipcodes { get; set; }
}

public class AddForecastCommandHandler : IRequestHandler<AddForecastCommand>
{
    private readonly Idotnet_semantickernelContext _context;

    public AddForecastCommandHandler(Idotnet_semantickernelContext context)
    {
        _context = context;
    }

    public async Task Handle(AddForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);
        GuardAgainstWeatherForecastNotFound(weatherForecast);

        var weatherForecastValue = ForecastValue.Create(request.Key, request.Date, (int) request.TemperatureF, request.Zipcodes);

        if (weatherForecastValue.IsFailure) 
            throw new Exception(weatherForecastValue.Error);

        _context.Forecasts.Add(new Forecast(weatherForecastValue.Value));

        await _context.SaveChangesAsync(CancellationToken.None);
    }

    private static void GuardAgainstWeatherForecastNotFound(Forecast? weatherForecast)
    {
        if (weatherForecast != null)
            throw new ValidationException(new List<ValidationFailure>
            {
                new("Key", "A Weather Forecast with this Key already exists")
            });
    }
}