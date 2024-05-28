using WeatherForecasts.Core.Application.Forecasts.Queries.Get;
using WeatherForecasts.Core.Domain.Forecasts.Entities;
using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Specs.Application.Unit.Forecasts.Queries.Get
{
    [Binding]
    [Scope(Tag = "getForecastQuery1")]
    public class GetForecastQueryStepDefinitions : TestBase
    {
        private Guid _forecastKey;
        private bool _forecastExists;
        private ForecastVm _response;
        private string _expectedSummaryResponse;
        private int _expectedTemperatureC;
        private int _expectedTemperatureF;

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
        }

        [Then(@"If the response is successful The response has a valid Summary of ""([^""]*)")]
        public void ThenIfTheResponseIsSuccessfulTheResponseHasAValidSummaryOfAnd(string expectedSummaryResponse)
        {
            _expectedSummaryResponse = expectedSummaryResponse;
        }

        [Given(@"I have a expected temperatureF response ""([^""]*)""")]
        public void GivenIHaveAExpectedTemperatureFResponse(int expectedTempF)
        {
            _expectedTemperatureF = expectedTempF;
        }

        [Given(@"I have a expected temperatureC response ""([^""]*)""")]
        public void GivenIHaveAExpectedTemperatureCResponse(int expectedTemperatureC)
        {
            _expectedTemperatureC = expectedTemperatureC;
        }

        [When(@"I get a forecast")]
        public async Task WhenIGetAWeatherForecast()
        {
            if (_forecastExists)
            {
                var weatherForecastView = new ForecastsView()
                {
                    ForecastDate = DateTime.UtcNow,
                    DateAdded = DateTime.UtcNow,
                    Key = _forecastKey,
                    Summary = _expectedSummaryResponse,
                    TemperatureF = _expectedTemperatureF,
                    PostalCodesSearch = "92673, 92674"
                };

                WeatherForecastsContext.ForecastViews.Add(weatherForecastView);
                await WeatherForecastsContext.SaveChangesAsync(CancellationToken.None);
            }

            var request = new GetWeatherForecastQuery()
            {
                Key = _forecastKey
            };

            var validator = new GetForecastQueryValidator();
            _validationResponse = validator.Validate(request);
            if (_validationResponse.IsValid)
                try
                {
                    var handler = new GetForecastQueryHandler(WeatherForecastsContext, Mapper);
                    _response = await handler.Handle(request, CancellationToken.None);
                    _responseType = CommandResponseType.Successful;
                }
                catch (Exception e)
                {
                   _responseType = HandleAssignResponseType(e);
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

            _response.TemperatureF.Should().Be(_expectedTemperatureF);
        }
        
        [Then(@"If the response is successful The response has a valid Summary")]
        public void ThenIfTheResponseIsSuccessfulTheResponseHasASummary()
        {
            if (_responseType != CommandResponseType.Successful) return;
            var validSummary = (ForecastEntity.SummaryType) Enum.Parse(typeof(ForecastEntity.SummaryType), _response.Summary);
            validSummary.ToString().Should().Be(_response.Summary);
        }

        [Then(@"If the response is successful The response has a Zipcodes")]
        public void ThenIfTheResponseIsSuccessfulTheResponseHasAZipcodes()
        {
            if (_responseType != CommandResponseType.Successful) return;
            _response.PostalCodes.Should().NotBeNull();
        }


        [Given(@"I have a expected summary response ""([^""]*)""")]
        public void GivenIHaveAExpectedSummaryResponse(string expectedSummaryResponse)
        {
            if (_responseType != CommandResponseType.Successful) return;
            
            _expectedSummaryResponse = expectedSummaryResponse;
        }

        [Then(@"If the response is successful The response has a valid Summary of ""([^""]*)""")]
        public void ThenIfTheResponseIsSuccessfulTheResponseHasAValidSummaryOf(string expectedSummaryResponse)
        {
            if (_responseType != CommandResponseType.Successful) return;
            var validSummary = (ForecastEntity.SummaryType)Enum.Parse(typeof(ForecastEntity.SummaryType), _response.Summary);
            _expectedSummaryResponse.ToString().Should().Be(_response.Summary);
        }
        
        [Then(@"If the response has validation issues I see the ""([^""]*)"" in the response")]
        public void ThenIfTheResponseHasValidationIssuesISeeTheInTheResponse(string expectedErrors)
        {
            HandleExpectedValidationErrorsAssertions(expectedErrors);
        }
    }
}
