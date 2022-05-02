
namespace GoodToCode.Shared.Patterns.Ddd
{
    public interface IDomainObject
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}