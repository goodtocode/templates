namespace $safeprojectname$
{
    public struct AppConfigurationKeys
    {
        public const string SentinelKey = "Shared:Sentinel";
        public const string StorageTablesConnectionString = "ConnectionStrings:StorageTablesConnection";
        public const string ServiceBusConnectionString = "ConnectionStrings:ServiceBusConnection";
        public const string BatchSize = "DurableTask:BatchSize";
        public const string HubName = "DurableTask:HubName";
    }
}
