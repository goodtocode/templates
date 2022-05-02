
namespace GoodToCode.Shared.Patterns.Ddd
{
    public interface IDomainEvent<T>
    {
        T Item { get; }
    }
}   