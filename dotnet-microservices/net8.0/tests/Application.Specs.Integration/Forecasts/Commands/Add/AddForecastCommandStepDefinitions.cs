using WeatherForecasts.Core.Application.Forecasts.Commands.Add;
using WeatherForecasts.Core.Domain.Forecasts.Entities;

namespace WeatherForecasts.Specs.Application.Integration.Forecasts.Commands.Add;

using static TestBase;

[Binding]
[Scope(Tag = "addForecastCommand")]
public class AddForecastCommandStepDefinitions : BaseTestFixture
{
    private Guid _forecastkey;
    private DateTime _forecastDate;
    private int? _tempF;
    private readonly List<int> _zipcodes = new();
    private bool _forecastExists;
    private string _def;

    [Given(@"I have a def ""([^""]*)""")]
    public async Task GivenIHaveADef(string def)
    {
        _def = def;
    }

    [Given(@"I have a forecast key ""([^""]*)""")]
    public void GivenIHaveAForecastKey(Guid forecastKey)
    {
        _forecastkey = forecastKey;
    }

    [Given(@"The forecast exists ""([^""]*)""")]
    public void GivenTheForecastExists(bool forecastExists)
    {
        _forecastExists = forecastExists;
    }

    [Given(@"I have a forecast date ""([^""]*)""")]
    public void GivenIHaveAForecastDate(DateTime forecastDate)
    {
        _forecastDate = forecastDate;
    }

    [Given(@"I have a forecast TemperatureF ""([^""]*)""")]
    public void GivenIHaveAForecastTemperatureF(int? tempF)
    {
        _tempF = tempF;
    }

    [Given(@"I have a collection of Zipcodes ""([^""]*)""")]
    public void GivenIHaveACollectionOfZipCodes(string zipCodes)
    {
        if (string.IsNullOrWhiteSpace(zipCodes)) return;

        var zipcodes = zipCodes.Split(',');

        foreach (var zipcode in zipcodes) _zipcodes.Add(int.Parse(zipcode));
    }

    [When(@"I add the forecast")]
    public async Task WhenIAddTheForecast()
    {
        await TestSetUp();

        if (_forecastExists)
        {
            var forecastAddValue = ForecastValue.Create(_forecastkey, DateTime.Now, 75, _zipcodes);
            var forecast = new Forecast(forecastAddValue.Value);
            await AddAsync(forecast);
        }

        var request = new AddForecastCommand
        {
            Key = _forecastkey,
            Date = _forecastDate,
            TemperatureF = _tempF,
            Zipcodes = _zipcodes
        };

        var validator = new AddForecastCommandValidator();
        ValidationResponse = await validator.ValidateAsync(request);

        if (ValidationResponse.IsValid)
            try
            {
                await SendAsync(request);
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

    [Then(@"If the response has validation issues I see the ""([^""]*)"" in the response")]
    public void ThenIfTheResponseHasValidationIssuesISeeTheInTheResponse(string expectedErrors)
    {
        HandleExpectedValidationErrorsAssertions(expectedErrors);
    }
}