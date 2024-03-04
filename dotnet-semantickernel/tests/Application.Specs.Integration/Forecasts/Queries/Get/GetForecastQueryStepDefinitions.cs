using dotnet_semantickernel.Core.Application.Forecasts.Queries.Get;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;

namespace dotnet_semantickernel.Specs.Application.Integration.Forecasts.Queries.Get;

using static TestBase;

[Binding]
[Scope(Tag = "getForecastQuery")]
public class GetForecastQueryStepDefinitions : BaseTestFixture
{
    private Guid _forecastKey;
    private bool _forecastExists;
    private ForecastVm _response;
    private string _expectedSummaryResponse;
    private int _expectedTemperatureC;
    private int _expectedTemperatureF;
    public string _def { get; set; } = "";

    [Given(@"I have a definition ""([^""]*)""")]
    public void GivenIHaveADefinition(string def)
    {
        _def = def;
    }

    [Given(@"I have a forecast key ""([^""]*)""")]
    public void GivenIHaveAForecastKey(Guid forecastKey)
    {
        _forecastKey = forecastKey;
    }

    [Given(@"I have a forecast exists ""([^""]*)""")]
    public void GivenIHaveAForecastExists(bool forecastExists)
    {
        _forecastExists = forecastExists;
        if (!_forecastExists)
        {
        }
    }

    [Given(@"I have a expected temperatureC ""([^""]*)""")]
    public void GivenIHaveAExpectedTemperatureCResponse(int expectedTemperatureC)
    {
        _expectedTemperatureC = expectedTemperatureC;
    }

    [Then(@"If the response is successful The response has a valid Summary of ""([^""]*)")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasAValidSummaryOfAnd(string expectedSummaryResponse)
    {
        _expectedSummaryResponse = expectedSummaryResponse;
    }

    [When(@"I get a forecast")]
    public async Task WhenIGetAWeatherForecast()
    {
        await TestSetUp();

        if (_forecastExists)
        {
            var forecastAddValue = ForecastValue.Create(_forecastKey, DateTime.UtcNow, _expectedTemperatureF,
                new List<int>
                {
                    92673, 92674
                });
            var forecast = new Forecast(forecastAddValue.Value);
            await AddAsync(forecast);
        }

        var request = new GetWeatherForecastQuery
        {
            Key = _forecastKey
        };

        var validator = new GetForecastQueryValidator();
        ValidationResponse = validator.Validate(request);
        if (ValidationResponse.IsValid)
            try
            {
                _response = await SendAsync(request);
                _responseType = CommandResponseType.Successful;
            }
            catch (Exception e)
            {
                _responseType = CreateCommandResponseType(e);
            }
        else
            _responseType = CommandResponseType.BadRequest;
    }

    [Then(@"The response is ""([^""]*)""")]
    public void ThenTheResponseIs(string response)
    {
        HandleHasResponseType(response);
    }

    [Then(@"If the response is successful the response has a Key")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasAKey()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.Key.Should().NotBeEmpty();
    }

    [Then(@"If the response is successful The response has a Date")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasADate()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.Date.Should().NotBe(DateTime.MinValue);
    }

    [Then(@"If the response is successful The response has a TemperatureF")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasATemperatureF()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.TemperatureF.Should();
    }

    //[Then(@"If the response is successful The response has a TemperatureF")]
    //public void ThenIfTheResponseIsSuccessfulTheResponseHasATemperatureF()
    //{
    //    if (_responseType != CommandResponseType.Successful) return;
    //    _response.TemperatureF.Should().Be(32 + (int)(_response.TemperatureC / 0.5556));
    //}

    [Given(@"I have a expected temperatureF ""([^""]*)""")]
    public void GivenIHaveAExpectedTemperatureC(int expectedTemperatureC)
    {
        expectedTemperatureC.Should().Be(expectedTemperatureC);

        if (_responseType != CommandResponseType.Successful) return;
        _response.TemperatureF.Should().Be(32 + (int) (_response.TemperatureC / 0.5556));
    }

    [Then(@"If the response is successful The response has a valid Summary")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasASummary()
    {
        if (_responseType != CommandResponseType.Successful) return;
        var validSummary = (Forecast.SummaryType) Enum.Parse(typeof(Forecast.SummaryType), _response.Summary);
        validSummary.ToString().Should().Be(_response.Summary);
    }

    [Given(@"I have a expected summary response ""([^""]*)""")]
    [Given(@"I have a expectedSummaryResponse ""([^""]*)""")]
    public void GivenIHaveAExpectedSummaryResponse(string expectedSummaryResponse)
    {
        if (_responseType != CommandResponseType.Successful) return;
        _expectedSummaryResponse = expectedSummaryResponse;
    }

    [Then(@"If the response is successful The response has a Zipcodes")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasAZipcodes()
    {
        if (_responseType != CommandResponseType.Successful) return;
        _response.Zipcodes.Should().NotBeNull();
    }

    [Then(@"If the response is successful The response has a valid Summary of ""([^""]*)""")]
    public void ThenIfTheResponseIsSuccessfulTheResponseHasAValidSummaryOf(string expectedSummaryResponse)
    {
        if (_responseType != CommandResponseType.Successful) return;
        var validSummary = (Forecast.SummaryType) Enum.Parse(typeof(Forecast.SummaryType), _response.Summary);

        _expectedSummaryResponse.Should().Be(validSummary.ToString());
    }

    [Then(@"If the response has validation issues I see the ""([^""]*)"" in the response")]
    public void ThenIfTheResponseHasValidationIssuesISeeTheInTheResponse(string expectedErrors)
    {
        HandleExpectedValidationErrorsAssertions(expectedErrors);
    }
}