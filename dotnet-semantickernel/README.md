# Microservice Quick-Start
A simple Microservice solution including Domain Models, Aggregates, Persistence Repositories and an API presentation layer. This Microservice API is built using .NET 6 and Entity Framework Core.

## Prerequisites
You will need the following tools:
### Visual Studio
[Visual Studio Workload IDs](https://learn.microsoft.com/en-us/visualstudio/install/workload-component-id-vs-community?view=vs-2022&preserve-view=true)
```
winget install --id Microsoft.VisualStudio.2022.Community --override "--quiet --add Microsoft.Visualstudio.Workload.Azure --add Microsoft.VisualStudio.Workload.Data --add Microsoft.VisualStudio.Workload.ManagedDesktop --add Microsoft.VisualStudio.Workload.NetWeb"
```
### Or VS Code (code .)
```
winget install Microsoft.VisualStudioCode --override '/SILENT /mergetasks="!runcode,addcontextmenufiles,addcontextmenufolders"'
```

### .NET SDK (dotnet)
```
winget install Microsoft.DotNet.SDK.8 --silent
```
### SQL Server
[Optional: SQL Server 2022 or above](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Configurations
Follow these steps to get your development environment set up:

  ### ASPNETCORE_ENVIRONMENT set to "Local" in launchsettings.json
	1. This project uses the following ASPNETCORE_ENVIRONMENT to set configuration profile
	- Debugging uses Properties/launchSettings.json
	- launchSettings.json is set to Local, which relies on appsettings.Local.json
	2. As a standard practice, set ASPNETCORE_ENVIRONMENT entry in your Enviornment Variables and restart Visual Studio
	```
	Set-Item -Path Env:ASPNETCORE_ENVIRONMENT -Value "Development"
	Get-Childitem env:
	```	
  
  ### Setup Azure Open AI or Open AI configuration
  #### Azure Open AI
	```
	cd src/Presentation/WebAPI
	dotnet user-secrets init
	dotnet user-secrets set "AzureOpenAI:ChatDeploymentName" "gpt-4"
	dotnet user-secrets set "AzureOpenAI:Endpoint" "https://YOUR_ENDPOINT.openai.azure.com/"
	dotnet user-secrets set "AzureOpenAI:ApiKey" "YOUR_API_KEY"
	```
	Alternately you can set in Environment variables
	```
	AzureOpenAI__ChatDeploymentName
	AzureOpenAI__Endpoint
	AzureOpenAI__ApiKey
	```

  #### Open AI
	```
	cd src/Presentation/WebAPI
	dotnet user-secrets init
	dotnet user-secrets set "OpenAI:ChatModelId" "gpt-3.5-turbo"
	dotnet user-secrets set "OpenAI:ApiKey" "YOUR_API_KEY"
	```
	Alternately you can set in Environment variables
	```
	OpenAI__ChatModelId	
	OpenAI__ApiKey
	```
  
  ### Setup your SQL Server connection string
	```
	dotnet user-secrets init
	dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_SQL_CONNECTION_STRING"
	```

  ### Launch the backend
	Right-click Presentation.WebApi and select Set as Default Project
     ```
	 dotnet run WebApi.csproj
	 ```

  ### Open http://localhost:7777/swagger/index.html in your browser to the Swagger API Interface
	Open Microsoft Edge or modern browser
	Navigate to: http://localhost:7777/swagger/index.html
  
## dotnet ef migrate steps
	1. Open Windows Terminal in Powershell or Cmd mode
	2. cd to /src folder
	3. (Optional) If you have an existing database, scaffold current entities into your project
	```
	dotnet ef dbcontext scaffold "Data Source=localhost;Initial Catalog=weather.test;Min Pool Size=3;MultipleActiveResultSets=True;Trusted_Connection=Yes;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -t WeatherForecastView -c WeatherChannelContext -f -o WebApi
	```
	4. Create an initial migration
	```
	dotnet ef migrations add InitialCreate
	```
	5. Develop new entities and configurations
	6. When ready to deploy new entities and configurations
	```
	dotnet ef database update
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
	dotnet new gtc-msv3 -o "MyOrg.DomainMicroservice"

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