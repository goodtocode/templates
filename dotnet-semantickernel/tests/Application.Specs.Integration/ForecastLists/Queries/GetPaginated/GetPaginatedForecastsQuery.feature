@getPaginatedForecastsQuery
Feature: Get Weather Forecasts Paginated Query
As a weather channel user
I can get a paginated collection of forecasts

Scenario: Get paginated weather forecasts
	Given I have a definition "<def>"
	And A collection of Forecasts Exist "<forecastsExist>"
	And I have a page number "<pageNumber>"
	And I have a page size "<pageSize>"
	When I get paginated forecasts
	Then The response is "<response>"
	And The response has a collection of forecasts
	And The response has a Page Number
	And The response has a Total Pages
	And The response has a Total Count
	And The response has a collection of items
	And Each forecast has a Key
	And Each forecast has a Date
	And Each forecast has a TemperatureC
	And Each forecast has a TemperatureF
	And Each forecast has a Summary
	And Each forecast has a Zipcodes
	Then The response is "<response>"
	And If the response has validation issues I see the "<responseErrors>" in the response

Examples:
	| def                          | response   | responseErrors | pageNumber | pageSize | forecastsExist |
	| success                      | Success    |                | 1          | 10       | true           |
	| success filtered results     | Success    |                | 1          | 10       | true           |
	| success empty results        | Success    |                | 1          | 10       | false          |
	| bad request page number zero | BadRequest | PageNumber     | 0          | 10       | true           |
	| bad request page size zero   | BadRequest | PageSize       | 1          | 0        | true           |