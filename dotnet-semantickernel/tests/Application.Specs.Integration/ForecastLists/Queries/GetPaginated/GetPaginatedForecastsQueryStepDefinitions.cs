using dotnet_semantickernel.Core.Application.Common.Exceptions;
using dotnet_semantickernel.Core.Application.Common.Models;
using dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetPaginated;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;
using dotnet_semantickernel.Core.Domain.Forecasts.Models;

namespace dotnet_semantickernel.Specs.Application.Integration.ForecastLists.Queries.GetPaginated;

using static TestBase;

[Binding]
[Scope(Tag = "getPaginatedForecastsQuery")]
public class GetPaginatedForecastsQueryStepDefinitions : BaseTestFixture
{
    private string _def;
    private bool _foreacastExists;
    private PaginatedList<ForecastPaginatedDto> _response;
    private string _zipcodeFilter;
    private int _pageNumber;
    private int _pageSize;

    [Given(@"I have a definition ""([^""]*)""")]
    public void GivenIHaveADefinition(string def)
    {
        _def = def;
    }

    [Given(@"I have a page number ""([^""]*)""")]
    public void GivenIHaveAPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
    }

    [Given(@"I have a page size ""([^""]*)""")]
    public void GivenIHaveAPageSize(int pageSize)
    {
        _pageSize = pageSize;
    }

    [Given(@"I have a forecast exists ""([^""]*)""")]
    public void GivenIHaveAForecastExists(bool forecastExists)
    {
        _foreacastExists = true;
    }

    [Given(@"A collection of Forecasts Exist ""([^""]*)""")]
    public void GivenACollectionOfForecastsExist(bool forecastExists)
    {
        _foreacastExists = forecastExists;
    }

    [When(@"I get paginated forecasts")]
    public async Task WhenIGetPaginatedForecasts()
    {
        await TestSetUp();
        if (_foreacastExists)
            for (var i = 0; i < 100; i++)
            {
                var forecastKey = Guid.NewGuid();
                var weatherForecastAddValue = ForecastValue.Create(forecastKey,
                    DateTime.Now.AddDays(i).ToUniversalTime(), Random.Shared.Next(-20, 55), new List<int>
                    {
                        92673, 92672
                    });
                var weatherForecast = new Forecast(weatherForecastAddValue.Value);
                await AddAsync(weatherForecast);
            }

        var request = new GetForecastsPaginatedQuery
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize
        };

        var validator = new GetForecastsPaginatedQueryValidator();

        ValidationResponse = validator.Validate(request);

        if (ValidationResponse.IsValid)
            try
            {
                _response = await SendAsync(request);
                _responseType = CommandResponseType.Successful;
            }
            catch (Exception e)
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
            }
        else
            _responseType = CommandResponseType.BadRequest;
    }

    private static ForecastsView CreateIncrementedForecast(int i)
    {
        var weatherForecastView = new ForecastsView
        {
            Key = Guid.NewGuid(),
            DateAdded = DateTime.Now,
            TemperatureF = 75,
            Summary = "summary",
            ZipCodesSearch = $"9260{i}"
        };
        return weatherForecastView;
    }

    private ForecastsView CreateForecastWithExistingZipcode()
    {
        var weatherForecastView = new ForecastsView
        {
            Key = Guid.NewGuid(),
            DateAdded = DateTime.Now,
            TemperatureF = 75,
            Summary = "summary",
            ZipCodesSearch = _zipcodeFilter
        };
        return weatherForecastView;
    }

    [Then(@"The response is ""([^""]*)""")]
    public void ThenTheResponseIs(string response)
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

    [Then(@"The response has a Page Number")]
    public void ThenTheResponseHasAPageNumber()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.PageNumber.Should();
    }

    [Then(@"The response has a Total Pages")]
    public void ThenTheResponseHasATotalPages()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.TotalPages.Should();
    }

    [Then(@"The response has a Total Count")]
    public void ThenTheResponseHasATotalCount()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.TotalCount.Should();
    }

    [Then(@"The response has a collection of items")]
    public void ThenTheResponseHasACollectionOfItems()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.Items.Should();
    }

    [Then(@"The response has a collection of forecasts")]
    public void ThenTheResponseHasACollectionOfForecasts()
    {
        if (_responseType != CommandResponseType.Successful) return;

        if (_foreacastExists)
            _response.Items.Any().Should().BeTrue();
        else
            _response.Items.Any().Should().BeFalse();
    }

    [Then(@"Each forecast has a Key")]
    public void ThenEachForecastHasAKey()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.Key.Should().NotBeEmpty();
    }

    [Then(@"Each forecast has a Date")]
    public void ThenEachForecastHasADate()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.Date.Should();
    }

    [Then(@"Each forecast has a TemperatureC")]
    public void ThenEachForecastHasATemperatureC()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.TemperatureC.Should();
    }

    [Then(@"Each forecast has a TemperatureF")]
    public void ThenEachForecastHasATemperatureF()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.TemperatureF.Should();
    }

    [Then(@"Each forecast has a Summary")]
    public void ThenEachForecastHasASummary()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.Summary.Should();
    }

    [Then(@"Each forecast has a Zipcodes")]
    public void ThenEachWeatherForecastHasAZipcodes()
    {
        if (_responseType != CommandResponseType.Successful) return;
        foreach (var forecast in _response.Items) forecast.ZipCodes.Should();
    }

    [Then(@"If the response has validation issues I see the ""([^""]*)"" in the response")]
    public void ThenIfTheResponseHasValidationIssuesISeeTheInTheResponse(string expectedErrors)
    {
        HandleExpectedValidationErrorsAssertions(expectedErrors);
    }
}