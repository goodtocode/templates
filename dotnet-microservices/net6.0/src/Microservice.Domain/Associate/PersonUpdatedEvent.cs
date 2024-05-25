using GoodToCode.Shared.Domain;

namespace Microservice.Domain
{
    public sealed class PersonUpdatedEvent : IDomainEvent<IPerson>
    {
        public IPerson Item { get; }

        public PersonUpdatedEvent(IPerson item)
        {
            Item = item;
        }
    }
}
