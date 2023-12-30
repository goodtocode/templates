﻿using dotnet_semantickernel.Core.Application.Common.Models;
using dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetAll;
using dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetPaginated;
using dotnet_semantickernel.Presentation.WebApi.Common;

namespace dotnet_semantickernel.Presentation.WebApi.ForecastsLists;

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
    public async Task<ActionResult<PaginatedList<ForecastPaginatedDto>>> Getdotnet_semantickernelPaginatedQuery([FromQuery] GetForecastsPaginatedQuery query)
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
    public async Task<ForecastsVm> GetAll(string zipcodeFilter)
    {
        return await Mediator.Send(new GetAllForecastsQuery()
        {
            ZipcodeFilter = zipcodeFilter
        });
    }


}