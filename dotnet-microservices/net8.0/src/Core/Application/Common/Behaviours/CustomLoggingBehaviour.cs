﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace WeatherForecasts.Core.Application.Common.Behaviours;

public class CustomLoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;


    public CustomLoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Weather Forecasts Request: {Name} {@Request}",
            requestName, request);
    }
}