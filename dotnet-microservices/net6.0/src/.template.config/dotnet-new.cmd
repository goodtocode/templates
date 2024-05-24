REM https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates

REM Lists installed templates
dotnet new --list

REM Searches for marketplace templates
dotnet new --search

REM Install/unisntall from Solution folder (Windows)
dotnet new --install .\
dotnet new --uninstall .\