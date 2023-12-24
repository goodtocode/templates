@getForecastQuery
Feature: Get Forecast Query
As a weather forecasts user
I can get a forecast

Scenario: Get forecast query
	Given I have a definition "<def>"
	And I have a forecast key "<key>"
	And I have a forecast exists "<forecastExists>"
	And I have a expected temperatureC "<expectedTemperatureC>"
	And I have a expected summary response "<expectedSummaryResponse>"
	When I get a forecast
	Then The response is "<response>"
	And If the response is successful the response has a Key
	And If the response is successful The response has a Date
	And If the response is successful The response has a TemperatureF
	And If the response is successful The response has a Zipcodes
	And If the response is successful The response has a valid Summary
	And If the response has validation issues I see the "<responseErrors>" in the response

Examples:
	| def                    | response   | responseErrors | key                                  | forecastExists | expectedTemperatureC | expectedSummaryResponse |
	| success Freezing       | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed1 | true           | 32                   | Freezing                |
	| success Bracing        | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed2 | true           | 33                   | Bracing                 |
	| success Chilly         | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed3 | true           | 41                   | Chilly                  |
	| success Cool           | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed4 | true           | 51                   | Cool                    |
	| success Mild           | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed5 | true           | 61                   | Mild                    |
	| success Warm           | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed6 | true           | 71                   | Warm                    |
	| success Balmy          | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed7 | true           | 81                   | Balmy                   |
	| success Hot            | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed8 | true           | 91                   | Hot                     |
	| success Sweltering     | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fed9 | true           | 101                  | Sweltering              |
	| success Scorching      | Success    |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fe10 | true           | 111                  | Scorching               |
	| not found              | NotFound   |                | 038d8e7f-f18f-4a8e-8b3c-3b6a6889fe11 | false          | 0                    | Cool                    |
	| bad request: empty key | BadRequest | Key            |                                      | false          | 0                    | Cool                    |