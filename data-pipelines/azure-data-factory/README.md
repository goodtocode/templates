# Azure Data Factory Pipeline Example

This repository includes an example of a simple Data Pipeline using the following Azure resources:
* [Azure Active Directory](https://azure.microsoft.com/en-us/services/active-directory/)
* [Azure Data Factory](https://docs.microsoft.com/en-us/azure/data-factory/introduction)
* [Azure Functions (python)](https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference-python?tabs=asgi%2Cazurecli-linux%2Capplication-level)
* [Azure SQL Database](https://docs.microsoft.com/en-us/azure/azure-sql/database/sql-database-paas-overview?view=azuresql)

## Repo Contents
Path | Contents | Description
--- | --- | ---
.azure | json, bicep, ps1 | Infrastructure as Code (IaC) for the Azure resources in ARM and CLI format 
.azure-devops | yml, ps1 | Azure DevOps pipelines and scripts for CI/CD
src | src, vsix | Simple Durable Task Framework source and VSIX packaging
infrastructure | Azure ARM json | Azure Infrastructure required to host the items in this repo.
pipelines | Azure Devops yml | Azure DevOps Pipelines that build, test and deploy /infrastructure and **/src folders

## Prerequisites
You will need the following tools:
* [Visual Studio Code or 2022](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 6.0 or above](https://www.microsoft.com/net/download/dotnet-core/6.0)
https://docs.microsoft.com/en-us/cli/azure/install-azure-cli
https://docs.microsoft.com/en-us/powershell/azure/install-az-ps
https://github.com/Azure-Samples/functions-quickstarts-python

## Contact
* [GitHub Repo](https://www.github.com/goodtocode/templates)
* [@goodtocode](https://www.twitter.com/goodtocode)
* [github.com/goodtocode](https://www.github.com/goodtocode)