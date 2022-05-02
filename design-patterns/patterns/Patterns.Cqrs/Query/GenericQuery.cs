namespace GoodToCode.Templates.Patterns.Cqrs
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
