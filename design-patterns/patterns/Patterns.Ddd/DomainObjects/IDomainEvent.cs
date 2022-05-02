
namespace GoodToCode.Templates.Patterns.Ddd
{
    public interface IDomainEvent<T>
    {
        T Item { get; }
    }
}   