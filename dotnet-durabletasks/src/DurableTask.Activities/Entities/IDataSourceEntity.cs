using GoodToCode.Shared.Persistence.Abstractions;

namespace DurableTask.Activities
{
    public interface IDataSourceEntity : IEntity
    {
        string Status { get; }
        string Title { get; }
    }
}