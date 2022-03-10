# Quick-Start .NET Templates

This repository includes templates that can be cloned as working source code in C# .NET, or as an installable Visual Studio Extension Installer (VSIX) template, or as a template for you to build your own VSIX templates.

| Microservice Quick-Start | Durable Task Framework Quick-Start
--- | --- | ---
Build: | [![Build Status](https://dev.azure.com/GoodToCode/GoodToCode.com/_apis/build/status/gtg-rg-templates-microservices?branchName=main)](https://dev.azure.com/GoodToCode/GoodToCode.com/_build/latest?definitionId=80&branchName=main) | [![Build Status](https://dev.azure.com/GoodToCode/GoodToCode.com/_apis/build/status/gtc-rg-templates-durabletasks?branchName=main)](https://dev.azure.com/GoodToCode/GoodToCode.com/_build/latest?definitionId=81&branchName=main)
| Microservice template that contains a simple microservice solution and pre-setup projects. Built via DDD, CQRS on the SOLID porinciple. | Durable Task Framework (DTFx) template contains a simple Event Sourcing solution and pre-setup projects.

## Repo Contents
Path | Contents | Description
--- | --- | ---
dotnet-durabletasks | src, vsix | Simple Microservice source and VSIX packaging
dotnet-microservices | src, vsix | Simple Durable Task Framework source and VSIX packaging
infrastructure | Azure ARM json | Azure Infrastructure required to host the items in this repo.
pipelines | Azure Devops yml | Azure DevOps Pipelines that build, test and deploy /infrastructure and **/src folders

## Prerequisites
You will need the following tools:
* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 6.0 or above](https://www.microsoft.com/net/download/dotnet-core/6.0)

## Contact
* [GitHub Repo](https://www.github.com/goodtocode/templates)
* [@goodtocode](https://www.twitter.com/goodtocode)
* [github.com/goodtocode](https://www.github.com/goodtocode)

## Technologies
* .NET 6
* EF Core 6
* Azure Functions 4
* Durable Task Framework 2
