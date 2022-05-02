
namespace GoodToCode.Shared.Patterns.Cqrs
{
    public class GenericCommand<T>
    {
        public T Item { get; set; }

        public GenericCommand() { }

        public GenericCommand(T item)
        {
            Item = item;
        }
    }
}
