
using GoodToCode.Shared.Domain;
using Microservice.Domain;

namespace Microservice.Domain
{
    public sealed class PersonDeletedEvent : IDomainEvent<IPerson>
    {
        public IPerson Item { get; }

        public PersonDeletedEvent(IPerson item)
        {
            Item = item;
        }
    }
}
