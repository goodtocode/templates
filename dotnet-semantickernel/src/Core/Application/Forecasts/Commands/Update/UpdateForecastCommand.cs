using dotnet_semantickernel.Core.Application.Common.Exceptions;
using dotnet_semantickernel.Core.Application.Common.Interfaces;

namespace dotnet_semantickernel.Core.Application.Forecasts.Commands.Update;

public class UpdateForecastCommand : IRequest
{
    public Guid Key { get; set; }
    public DateTime Date { get; set; }
    public int? TemperatureF { get; set; }
    public List<int> Zipcodes { get; set; }
}

public class UpdateWeatherForecastCommandHandler : IRequestHandler<UpdateForecastCommand>
{
    private readonly Idotnet_semantickernelContext _context;

    public UpdateWeatherForecastCommandHandler(Idotnet_semantickernelContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);
        if (weatherForecast == null) throw new NotFoundException();

        weatherForecast.UpdateDate(request.Date);
        weatherForecast.UpdateTemperatureF((int) request.TemperatureF);
        weatherForecast.UpdateZipcodes(request.Zipcodes);
        _context.Forecasts.Update(weatherForecast);
        await _context.SaveChangesAsync(CancellationToken.None);
    }
}