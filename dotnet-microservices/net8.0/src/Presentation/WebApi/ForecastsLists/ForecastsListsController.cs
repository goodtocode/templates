﻿using WeatherForecasts.Core.Application.Common.Models;
using WeatherForecasts.Core.Application.ForecastLists.Queries.GetAll;
using WeatherForecasts.Core.Application.ForecastLists.Queries.GetPaginated;
using WeatherForecasts.Presentation.WebApi.Common;

namespace WeatherForecasts.Presentation.WebApi.ForecastsLists;

/// <summary>
/// Controller for Forecasts
/// </summary>
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("[controller]")]
[ApiVersion("1.0")]
public class ForecastsListsController : ApiControllerBase
{
    /// <summary>Get Paginated Query</summary>
    /// <remarks>
    /// Sample request:
    ///
    ///        "PageNumber": 1
    ///        "PageSize" : 10
    ///        "api-version":  1.0
    /// 
    /// </remarks>
    /// <returns>ForecastVm</returns>
    [HttpGet("Paginated", Name = "GetForecastsPaginatedQuery")]
    [ProducesResponseType(typeof(PaginatedList<ForecastPaginatedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedList<ForecastPaginatedDto>>> GetWeatherForecastsPaginatedQuery([FromQuery] GetForecastsPaginatedQuery query)
    {
        return await Mediator.Send(query);
    }
    
    /// <summary>Get All Forecasts Query</summary>
    /// <remarks>
    /// Sample request:
    ///
    ///        "Key": 60fb5e99-3a78-43df-a512-7d8ff498499e
    ///        "api-version":  1.0
    /// 
    /// </remarks>
    /// <returns>ForecastVm</returns>
    [HttpGet(Name = "GetAllForecastsQuery")]
    [ProducesResponseType(typeof(ForecastsVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ForecastsVm> GetAll(string codeFilter)
    {
        return await Mediator.Send(new GetAllForecastsQuery()
        {
            PostalCodeFilter = codeFilter
        });
    }


}