using dotnet_semantickernel.Core.Application.Common.Exceptions;
using dotnet_semantickernel.Core.Application.Common.Interfaces;

namespace dotnet_semantickernel.Core.Application.Forecasts.Commands.Remove;

public class RemoveForecastCommand : IRequest
{
    public Guid Key { get; set; }
}

public class RemoveForecastCommandHandler : IRequestHandler<RemoveForecastCommand>
{
    private readonly Idotnet_semantickernelContext _context;

    public RemoveForecastCommandHandler(Idotnet_semantickernelContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);

        if (weatherForecast == null) throw new NotFoundException();
        _context.Forecasts.Remove(weatherForecast);
        await _context.SaveChangesAsync(cancellationToken);
    }
}