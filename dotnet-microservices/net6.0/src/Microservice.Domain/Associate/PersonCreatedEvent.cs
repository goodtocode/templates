using GoodToCode.Shared.Domain;
using Microservice.Domain;

namespace Microservice.Domain
{
    public sealed class PersonCreatedEvent : IDomainEvent<IPerson>
    {
        public IPerson Item { get; }

        public PersonCreatedEvent(IPerson item)
        {
            Item = item;
        }
    }
}
