# Microservice Quick-Start
A simple Microservice solution including Domain Models, Aggregates, Persistence Repositories and an API presentation layer. This Microservice API is built using .NET 6 and Entity Framework Core.

## Prerequisites
You will need the following tools:
* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 8.0 or above](https://www.microsoft.com/net/download/dotnet-core/6.0)
* [Optional: SQL Server 2022 or above](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Setup
Follow these steps to get your development environment set up:

  ### 1. Setup your local ASPNETCORE_ENVIRONMENT setting
     ```
	- Application layer uses ASPNETCORE_ENVIRONMENT environment variable, and will default to Production if not found.
	- Add the ASPNETCORE_ENVIRONMENT entry in your Enviornment Variables

	 To accopmlish this on Windows
	 1. Open Control Panel > System > >Advanced Settings > Environment Variables > System Variables
	 2. Next add new User Variable
		 UserVariables > New
				Variable name:  ASPNETCORE_ENVIRONMENT
				Variable value: Development
	 3. Then Ok (to save)
	 
	 You will need to restart Visual Studio, VS Code and Terminals to see the changes
	 ```

  ### 2. Setup your SQL Server connection string in appsettings.*.json
     ```
	- Application layer requires the following application configuration: ConnectionStrings.SqlConnection
	- Both JSON configuration (appsettings.*.json) and Azure App Configuration service are supported
	
	To accopmlish this in appsettings.*.json
        1. Open all instances of appsettings.Development.json and appsettings.Production.json
	2. Copy your SQL Server Connection String from the Azure Portal, or your on-premise SQL Server
	3. Paste your connection string over the following setting:
			"ConnectionStrings": {
				"SqlConnection": "YOUR_CONNECTION_STRING_HERE"
			}
	 4. Repeat for both Development and Production
	 5. Save all instances of appsettings.Development.json and appsettings.Production.json
     ```

  ### 3. Launch the backend, within the `Microservice.Api' directory
     ```
	 dotnet run Presentation.Api.csproj project (>Start)
	 ```

  ### 4. Open http://localhost:9023/index.html in your browser to the Swagger API Interface
  
  ### 5. Open http://localhost:9023/specs in your browser to view all CORE Specifications

  ### 6. Run any test scenerios within Microservice.Tests 
     ```
	 these specifications scenerios validate the infrastructure and presentation layer
     ```

## dotnet new steps
1. Start Windows Terminal
2. Navigate to template.json folder
```
cd ./dotnet-microservices/v3-webapi/.template/dotnet-new
```
3. Install Template command: 
```
dotnet new --install .
```
4. Create a folder where you're creating your new solution
```
mkdir /repos/dotnet-microservice
cd /repos/dotnet-microservice
```
5. Create microservice solution
dotnet new aca -o "MyNewCoolApp"

## Contact
* [GitHub Repo](https://www.github.com/goodtocode/templates)
* [@goodtocode](https://www.twitter.com/goodtocode)
* [github.com/goodtocode](https://www.github.com/goodtocode)

## Technologies
* [ASP.NET .Net](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [Specflow](https://specflow.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [FluentAssertions](https://fluentassertions.com/)
* [Moq](https://github.com/moq)

## Additional Technologies References
* AspNetCore.HealthChecks.UI
* Entity Framework Core
* FluentValidation.AspNetCore
* Microsoft.AspNetCore.App
* Microsoft.AspNetCore.Cors
* Swashbuckle.AspNetCore.SwaggerGen
* Swashbuckle.AspNetCore.SwaggerUI

This project is licensed with the [MIT license](LICENSE).