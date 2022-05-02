namespace GoodToCode.Shared.Patterns.Cqrs
{
    public class GenericQuery<TEntity>
    {
        public TEntity Item { get; }

        public GenericQuery() { }

        public GenericQuery(TEntity item)
        {
            Item = item;
        }
    }
}
