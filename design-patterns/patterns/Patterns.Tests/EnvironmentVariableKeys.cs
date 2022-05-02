using System;

namespace GoodToCode.Shared.Patterns
{
    public static class EnvironmentVariables
    {
        internal static void Validate()
        {
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppConfigurationConnection)))
                throw new ArgumentNullException(EnvironmentVariableKeys.AppConfigurationConnection);
        }
    }

    public struct EnvironmentVariableKeys
    {
        public const string AppConfigurationConnection = "GTC_SHARED_CONNECTION";
        public const string EnvironmentAspNetCore = "ASPNETCORE_ENVIRONMENT";
        public const string EnvironmentAzureFunctions = "AZURE_FUNCTIONS_ENVIRONMENT";
        public const string EnvironmentDotNet = "DOTNET_ENVIRONMENT";
    }

    public struct EnvironmentVariableDefaults
    {
        public const string Environment = "Production";
    }
}
