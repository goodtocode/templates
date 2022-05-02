using System;

namespace GoodToCode.Shared.Patterns.Ddd
{
    public interface IEntity
    {
        Guid RowKey { get; }
        string PartitionKey { get; }
    }
}