using GoodToCode.Shared.Persistence.Abstractions;

namespace $safeprojectname$
{
    public interface IDataSourceEntity : IEntity
    {
        string Status { get; }
        string Title { get; }
    }
}