using GoodToCode.Shared.Domain;
using Microservice.Domain;

namespace Microservice.Domain
{
    public sealed class BusinessCreatedEvent : IDomainEvent<IBusiness>
    {
        public IBusiness Item { get; }

        public BusinessCreatedEvent(IBusiness item)
        {
            Item = item;
        }
    }
}
