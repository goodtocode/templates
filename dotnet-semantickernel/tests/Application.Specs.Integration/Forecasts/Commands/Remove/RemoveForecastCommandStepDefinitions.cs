using dotnet_semantickernel.Core.Application.Forecasts.Commands.Remove;
using dotnet_semantickernel.Core.Domain.Forecasts.Entities;

namespace dotnet_semantickernel.Specs.Application.Integration.Forecasts.Commands.Remove;

using static TestBase;

[Binding]
[Scope(Tag = "removeForecastCommand")]
public class RemoveForecastCommandStepDefinitions : BaseTestFixture
{
    private bool _forecastExists;
    private Guid _forecastKey;
    private string _def;

    [Given(@"I have a def ""([^""]*)""")]
    public void GivenIHaveADef(string def)
    {
        _def = def;
    }

    [Given(@"I have a forecast key""([^""]*)""")]
    public void GivenIHaveAForecastKey(Guid key)
    {
        _forecastKey = key;
    }

    [Given(@"The forecast exists ""([^""]*)""")]
    public void GivenTheForecastExists(bool forecastExists)
    {
        _forecastExists = forecastExists;
    }

    [When(@"I remove the forecast")]
    public async Task WhenIRemoveTheForecast()
    {
        await TestSetUp();

        if (_forecastExists)
        {
            var forecastAddValue =
                ForecastValue.Create(_forecastKey, DateTime.Now, 75, new List<int> {99999});
            var weatherForecast = new Forecast(forecastAddValue.Value);
            await AddAsync(weatherForecast);
        }

        var request = new RemoveForecastCommand
        {
            Key = _forecastKey
        };

        var validator = new RemoveForecastCommandValidator();
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