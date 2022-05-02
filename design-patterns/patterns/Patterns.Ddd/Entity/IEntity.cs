using System;

namespace GoodToCode.Templates.Patterns.Ddd
{
    public interface IEntity
    {
        Guid RowKey { get; }
        string PartitionKey { get; }
    }
}