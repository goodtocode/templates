using dotnet_semantickernel.Core.Application.Common.Exceptions;
using dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetAll;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;

namespace dotnet_semantickernel.Specs.Application.Integration.ForecastLists.Queries.GetAll;

using static TestBase;

[Binding]
[Scope(Tag = "getAlldotnet_semantickernelQuery")]
public class GetAllForecastsQueryStepDefinitions : BaseTestFixture
{
    private string _def;
    private bool _foreacastExists;
    private bool _forecastWithZipcodeFilterExists;
    private string _zipcodeFilter;
    private ForecastsVm _response;

    [Given(@"I have a definition ""([^""]*)""")]
    public void GivenIHaveADefinition(string def)
    {
        _def = def;
    }

    [Given(@"Forecasts Exist ""([^""]*)""")]
    public void GivenForecastsExist(bool forecastExists)
    {
        _foreacastExists = forecastExists;
    }

    [Given(@"I have a zipcode filter ""([^""]*)""")]
    public void GivenIHaveAZipcodeFilter(string zipcodeFilter)
    {
        _zipcodeFilter = zipcodeFilter;
    }

    [Given(@"A forecast with zipcodeFilter exists ""([^""]*)""")]
    public void GivenAForecastWithZipcodeFilterExists(bool forecastWithZipcodeFilterExists)
    {
        _forecastWithZipcodeFilterExists = forecastWithZipcodeFilterExists;
    }

    [Given(@"I have a forecast exists ""([^""]*)""")]
    public void GivenIHaveAForecastExists(bool forecastExists)
    {
        _foreacastExists = true;
    }

    [When(@"I get all forecasts")]
    public async Task WhenIGetAWeatherForecast()
    {
        await TestSetUp();

        if (_foreacastExists)
            for (var i = 0; i < 2; i++)
            {
                var zip = 92600;
                var forecastKey = Guid.NewGuid();
                var weatherForecastAddValue = ForecastValue.Create(forecastKey,
                    DateTime.Now.AddDays(i).ToUniversalTime(), Random.Shared.Next(-20, 55), new List<int>
                    {
                        zip + 1, zip + 2
                    });
                var weatherForecast = new Forecast(weatherForecastAddValue.Value);
                await AddAsync(weatherForecast);
            }

        var request = new GetAllForecastsQuery { ZipcodeFilter = _zipcodeFilter };

        var validator = new GetAllForecastsQueryValidator();

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

    [Then(@"The response has a collection of forecasts")]
    public void ThenTheResponseHasACollectionOfdotnet_semantickernel()
    {
        if (_foreacastExists)
            _response.Forecasts.Any().Should().BeTrue();
        else
            _response.Forecasts.Any().Should().BeFalse();
    }

    [Then(@"Each forecast has a Key")]
    public void ThenEachForecastHasAKey()
    {
        foreach (var forecast in _response.Forecasts) forecast.Key.Should().NotBeEmpty();
    }

    [Then(@"Each forecast has a Date")]
    public void ThenEachForecastHasADate()
    {
        foreach (var forecast in _response.Forecasts) forecast.Date.Should();
    }

    [Then(@"Each forecast has a TemperatureC")]
    public void ThenEachForecastHasATemperatureC()
    {
        foreach (var forecast in _response.Forecasts) forecast.TemperatureC.Should();
    }

    [Then(@"Each forecast has a TemperatureF")]
    public void ThenEachForecastHasATemperatureF()
    {
        foreach (var forecast in _response.Forecasts) forecast.TemperatureF.Should();
    }

    [Then(@"Each forecast has a Summary")]
    public void ThenEachForecastHasASummary()
    {
        foreach (var forecast in _response.Forecasts) forecast.Summary.Should();
    }

    [Then(@"Each forecast has a Zipcodes")]
    public void ThenEachWeatherForecastHasAZipcodes()
    {
        foreach (var forecast in _response.Forecasts) forecast.Should();
    }
}