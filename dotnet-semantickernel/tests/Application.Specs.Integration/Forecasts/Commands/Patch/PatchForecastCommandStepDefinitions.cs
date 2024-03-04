using dotnet_semantickernel.Core.Application.Forecasts.Commands.Patch;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;

namespace dotnet_semantickernel.Specs.Application.Integration.Forecasts.Commands.Patch;

using static TestBase;

[Binding]
[Scope(Tag = "patchForecastCommand")]
public class PatchForecastCommandStepDefinitions : BaseTestFixture
{
    private DateTime _forecastDate;
    private bool _forecastExists;
    private Guid _forecastKey;
    private int? _temperatureC;
    private List<int> _zipcodes = new();
    private string _def;

    [Given(@"I have a def ""([^""]*)""")]
    public async Task GivenIHaveADef(string def)
    {
        _def = def;
    }

    [Given(@"I have a forecast key ""([^""]*)""")]
    public void GivenIHaveAForecastKey(Guid forecastKey)
    {
        _forecastKey = forecastKey;
    }

    [Given(@"I have a forecast date ""([^""]*)""")]
    public void GivenIHaveForecastDate(DateTime forecastDate)
    {
        _forecastDate = forecastDate;
    }

    [Given(@"I have a forecast TemperatureF ""([^""]*)""")]
    public void GivenIHaveAForecastTemperatureF(string temperatureF)
    {
        if (temperatureF != string.Empty) _temperatureC = int.Parse(temperatureF);
    }

    [Given(@"I have a collection of Zipcodes ""([^""]*)""")]
    public void GivenIHaveACollectionOfZipCodes(string zipCodes)
    {
        if (zipCodes == string.Empty)
            return;

        var zipCodesParsed = zipCodes.Split(",");
        _zipcodes = zipCodesParsed.Select(int.Parse).ToList();
    }

    [Given(@"the forecast exists ""([^""]*)""")]
    public void GivenTheForecastExists(bool forecastExists)
    {
        _forecastExists = forecastExists;
    }

    [When(@"I patch the forecast")]
    public async Task WhenIUpdateTheForecast()
    {
        await TestSetUp();

        if (_forecastExists)
        {
            var forecastValue =
                ForecastValue.Create(_forecastKey, DateTime.Now.AddDays(-1), 75, new List<int> {90000, 90002});
            var weatherForecast = new Forecast(forecastValue.Value);
            await AddAsync(weatherForecast);
        }

        var request = new PatchForecastCommand
        {
            Key = _forecastKey,
            Date = _forecastDate,
            TemperatureC = _temperatureC,
            Zipcodes = _zipcodes
        };

        var validator = new PatchForecastCommandValidator();
        ValidationResponse = await validator.ValidateAsync(request);

        if (ValidationResponse.IsValid)
            try
            {
                await SendAsync(request);
                _responseType = CommandResponseType.Successful;
            }
            catch (Exception e)
            {
                CreateCommandResponseType(e);
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