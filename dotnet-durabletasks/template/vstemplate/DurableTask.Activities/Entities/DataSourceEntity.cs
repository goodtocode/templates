using System;

namespace $safeprojectname$
{
    public class DataSourceEntity : IDataSourceEntity
    {
        public string Status { get; }

        public string Title { get; }

        public string RowKey { get; }

        public string PartitionKey { get; }

        public DateTimeOffset? Timestamp { get; }
    }
}